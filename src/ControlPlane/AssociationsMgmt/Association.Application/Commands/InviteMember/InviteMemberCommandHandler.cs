
using Association.Application.Contracts;
using Association.Application.Messages;
using Association.Application.Services;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using SharedKernel.ValueObjects;
using System.Security.Cryptography;

namespace Association.Application.Commands.InviteMember;

public sealed class InviteMemberCommandHandler(
    ICurrentTenant currentTenant,
    ICurrentTenantUser currentTenantUser,
    IAssociationRepository associationRepository,
    IAssociationInvitationRepository invitationRepository,
    AssociationMemberCapacityChecker capacityChecker,
    IAssociationOutbox outbox)
    : ICommandHandler<InviteMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        InviteMemberCommand command,
        CancellationToken ct)
    {
        // STEP 1: Verify that the current user is authenticated
        // and has permission to invite members.
        EnsureAuthenticated();
        EnsureAssociationAdmin();

        // STEP 2: Get the current Association and its tier.
        var associationId = GetCurrentAssociationId();
        var tierId = GetCurrentTierId();

        // STEP 3: Convert the supplied email into our value object.
        var email = new ContactEmail(command.Email);

        // STEP 4: Check whether the Association has capacity
        // for another member.
        await capacityChecker.EnsureCapacityAvailableAsync(
            associationId,
            tierId,
            ct);

        // STEP 5: Make sure this email isn't already a member
        // or the subject of another pending invitation.
        await EnsureInviteeCanBeInvitedAsync(
            associationId,
            email,
            ct);

        // STEP 6: Generate a secure invitation token.
        var token = GenerateInvitationToken();

        // STEP 7: Create the invitation.
        // Only the HASH of the token is stored in the database.
        var invitation = AssociationInvitation.Create(
            associationId,
            new UserID(currentTenantUser.UserId),
            email,
            InvitationTokenHash.Create(token));

        // STEP 8: Add the invitation to the DbContext.
        // It has not been saved yet.
        await invitationRepository.AddAsync(invitation, ct);

        // STEP 9: Publish a request to send the invitation email.
        // Wolverine holds this outgoing message in the Outbox.
        await outbox.PublishAsync(
            new SendAssociationInvitationEmail(
                email.Value,
                token,
                associationId.Value));

        // STEP 10: Save the invitation and outgoing message together.
        // If the save fails, neither should be committed.
        await outbox.SaveChangesAndFlushMessagesAsync(ct);

        // STEP 11: Return the newly created invitation ID.
        return invitation.Id.Value;
    }

    private void EnsureAuthenticated()
    {
        if (!currentTenantUser.IsAuthenticated ||
            currentTenantUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to invite members.");
        }
    }

    private void EnsureAssociationAdmin()
    {
        if (!currentTenantUser.IsAssociationAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only an Association admin can invite members.");
        }
    }

    private AssociationID GetCurrentAssociationId()
    {
        if (currentTenant.AssociationId is not Guid id ||
            id == Guid.Empty)
        {
            throw new InvalidOperationException(
                "No Association is resolved for this request.");
        }

        return new AssociationID(id);
    }

    private Guid GetCurrentTierId()
    {
        if (currentTenant.TierId is not Guid id ||
            id == Guid.Empty)
        {
            throw new InvalidOperationException(
                "The current Association does not have a valid tier.");
        }

        return id;
    }

    private async Task EnsureInviteeCanBeInvitedAsync(
        AssociationID associationId,
        ContactEmail email,
        CancellationToken ct)
    {
        var isAlreadyMember =
            await associationRepository.IsMemberByEmailAsync(
                associationId,
                email,
                ct);

        if (isAlreadyMember)
        {
            throw new InvalidOperationException(
                "This email already belongs to a member of the Association.");
        }

        var hasPendingInvitation =
            await invitationRepository.IsPendingInvitationExistsAsync(
                associationId,
                email,
                ct);

        if (hasPendingInvitation)
        {
            throw new InvalidOperationException(
                "A pending invitation already exists for this email.");
        }
    }

    private static string GenerateInvitationToken()
    {
        var tokenBytes = RandomNumberGenerator.GetBytes(32);

        return Convert.ToBase64String(tokenBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');
    }
}
