using Association.Application.Contracts;
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
    IAssociationInvitationEmailSender emailSender)
    : ICommandHandler<InviteMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        InviteMemberCommand command,
        CancellationToken ct)
    {
        EnsureAuthenticated();
        EnsureAssociationAdmin();

        var associationId = GetCurrentAssociationId();
        var tierId = GetCurrentTierId();

        var email = new ContactEmail(command.Email);

        await capacityChecker.EnsureCapacityAvailableAsync(
            associationId,
            tierId,
            ct);

        await EnsureInviteeCanBeInvitedAsync(
            associationId,
            email,
            ct);

        var token = GenerateInvitationToken();

        var invitation = AssociationInvitation.Create(
            associationId,
            new UserID(currentTenantUser.UserId),
            email,
            InvitationTokenHash.Create(token));

        await invitationRepository.AddAsync(invitation, ct);
        await invitationRepository.SaveChangesAsync(ct);

        await emailSender.SendInvitationAsync(
            email.Value,
            token,
            associationId.Value,
            ct);

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