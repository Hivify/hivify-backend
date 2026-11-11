
using Association.Application.Contracts;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using SharedKernel.ValueObjects;
using System.Security.Cryptography;

namespace Association.Application.Commands.InviteMember;

public sealed class InviteMemberCommandHandler(
    ICurrentTenantUser currentTenantUser,
    IAssociationRepository associationRepository,
    IAssociationInvitationRepository invitationRepository,
    IAssociationTierProvider associationTierProvider,
    IAssociationInvitationEmailSender emailSender)
    : ICommandHandler<InviteMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        InviteMemberCommand command,
        CancellationToken ct)
    {
        // 1. Authenticate inviter.
        if (!currentTenantUser.IsAuthenticated ||
            currentTenantUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to invite members.");
        }

        var userId = currentTenantUser.UserId;

        // 2. Resolve current Association.
        if (currentTenantUser.AssociationId is not Guid associationId ||
            associationId == Guid.Empty)
        {
            throw new InvalidOperationException(
                "No association is resolved for this request.");
        }

        var associationID = new AssociationID(associationId);

        // 3. Verify inviter's role.
        if (!currentTenantUser.IsAssociationAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only an association admin can invite members.");
        }

        // 4. Load Association to obtain its TierId.
        var association = await associationRepository.GetByIdAsync(
            associationID,
            ct);

        if (association is null)
        {
            throw new InvalidOperationException(
                "The current association could not be found.");
        }

        // 5. Retrieve the policy for the Association's selected tier.
        var tierPolicy = await associationTierProvider.GetPolicyAsync(
            association.TierId.Value,
            ct);

        if (tierPolicy is null)
        {
            throw new InvalidOperationException(
                "The association's tier is unavailable.");
        }

        // 6. Check member capacity.
        var memberCount = await associationRepository.GetMemberCountAsync(
            associationID,
            ct);

        var pendingInvitationCount =
            await invitationRepository.GetPendingInvitationCountAsync(
                associationID,
                ct);

        if (memberCount + pendingInvitationCount >= tierPolicy.MaxMembers)
        {
            throw new InvalidOperationException(
                "Your association has reached the maximum member limit for its current tier.");
        }

        // 7. Validate invitee email.
        var email = new ContactEmail(command.Email);

        // 8. Reject invitations to existing members.
        var isAlreadyMember =
            await associationRepository.IsMemberByEmailAsync(
                associationID,
                email,
                ct);

        if (isAlreadyMember)
        {
            throw new InvalidOperationException(
                "This email already belongs to a member of the association.");
        }

        // 9. Reject duplicate pending invitations.
        var hasPendingInvitation =
            await invitationRepository.IsPendingInvitationExistsAsync(
                associationID,
                email,
                ct);

        if (hasPendingInvitation)
        {
            throw new InvalidOperationException(
                "A pending invitation already exists for this email.");
        }

        // 10. Generate a cryptographically secure token.
        var tokenBytes = RandomNumberGenerator.GetBytes(32);

        var invitationToken = Convert.ToBase64String(tokenBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

        var tokenHash = InvitationTokenHash.Create(invitationToken);

        var invitation = AssociationInvitation.Create(
            associationID,
            new UserID(userId),
            email,
            tokenHash);

        // 11. Persist invitation before sending email.
        await invitationRepository.AddAsync(invitation, ct);
        await invitationRepository.SaveChangesAsync(ct);

        // 12. Send raw token to invitee.
        await emailSender.SendInvitationAsync(
            email.Value,
            invitationToken,
            associationId,
            ct);

        return invitation.Id.Value;
    }
}
