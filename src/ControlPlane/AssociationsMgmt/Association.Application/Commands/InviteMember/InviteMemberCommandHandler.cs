using Association.Application.Context;
using Association.Application.Contracts;
using Association.Application.QueueMessages;
using Association.Application.Services;
using Association.Application.Utilities;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Messaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using SharedKernel.ValueObjects;

namespace Association.Application.Commands.InviteMember;

public sealed class InviteMemberCommandHandler(
    ICurrentAssociation currentAssociation,
    ICurrentTenantUser currentTenantUser,
    IAssociationRepository associationRepository,
    IAssociationInvitationRepository invitationRepository,
    AssociationMemberCapacityChecker capacityChecker,
    IOutbox outbox)
    : ICommandHandler<InviteMemberCommand, Guid>
{
    public async Task<Guid> Handle(InviteMemberCommand command, CancellationToken ct)
    {
        // STEP 1: Verify authentication and admin permissions.
        currentAssociation.EnsureAuthenticated();
        currentAssociation.EnsureAdmin();

        // STEP 2: Get the current Association and its tier.
        var associationId = currentAssociation.GetAssociationId();
        var tierId = currentAssociation.GetTierId();

        // STEP 3: Convert the supplied email into our value object.
        var email = new AssociationEmail(command.Email);

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
        var token = InvitationTokenGenerator.Generate();

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
        await outbox.SaveChangesAndFlushMessagesAsync(ct);

        // STEP 11: Return the newly created invitation ID.
        return invitation.Id.Value;
    }

    private async Task EnsureInviteeCanBeInvitedAsync(
        AssociationID associationId,
        AssociationEmail email,
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

}