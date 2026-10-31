
using Association.Application.Contracts;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.AssociationUserPort;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using SharedKernel.ValueObjects;
using System.Security.Cryptography;
using System.Text;

namespace Association.Application.Commands.InviteMember;

public sealed class InviteMemberCommandHandler(
    ICurrentAssociationMember currentTenant,
    IAssociationRepository associationRepository,
    IAssociationInvitationRepository invitationRepository,
    IAssociationInvitationEmailSender emailSender)
    : ICommandHandler<InviteMemberCommand, Guid>
{
    public async Task<Guid> Handle(
        InviteMemberCommand command,
        CancellationToken ct)
    {
        // 1. Ensure the request has an authenticated inviter.
        if (!currentTenant.IsAuthenticated ||
            currentTenant.UserId is not Guid userId ||
            userId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "An authenticated user is required to invite members.");
        }

        // 2. Resolve the association from the current tenant.
        if (currentTenant.AssociationId is not Guid associationId ||
            associationId == Guid.Empty)
        {
            throw new InvalidOperationException(
                "No association is resolved for this request.");
        }

        // 3. Verify the user's role in this association.
        if (!currentTenant.IsAssociationAdmin)
        {
            throw new UnauthorizedAccessException(
                "Only a TenantAdmin can invite members.");
        }

        // 4. Validate and wrap the email address.
        var email = new ContactEmail(command.Email);

        // 5. Reject invitations to existing members.
        var isAlreadyMember =
            await associationRepository.IsMemberByEmailAsync(
                new AssociationID(associationId),
                email,
                ct);

        if (isAlreadyMember)
        {
            throw new InvalidOperationException(
                "This email already belongs to a member of the association.");
        }

        // 6. Reject duplicate pending invitations.
        var hasPendingInvitation =
            await invitationRepository.IsPendingInvitationExistsAsync(
                new AssociationID(associationId),
                email.Value,
                ct);

        if (hasPendingInvitation)
        {
            throw new InvalidOperationException(
                "A pending invitation already exists for this email.");
        }

        // 7. Generate a cryptographically secure, single-use token.
        var tokenBytes = RandomNumberGenerator.GetBytes(32);
        var invitationToken = Convert.ToBase64String(tokenBytes)
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

        // . Store only the SHA-256 hash, never the raw token.
        var tokenHash = Convert.ToHexString(
            SHA256.HashData(
               Encoding.UTF8.GetBytes(invitationToken)));

        var expiresAt = DateTime.UtcNow.AddDays(7);

        var invitation = AssociationInvitation.Create(
            new AssociationID(associationId),
            new UserID(userId),
            email,
            tokenHash,
            expiresAt);

        // 8. Persist the invitation before sending the email.
        await invitationRepository.AddAsync(invitation, ct);
        await invitationRepository.SaveChangesAsync(ct);

        // 9. Send the raw token to the invitee.
        await emailSender.SendInvitationAsync(
            email.Value,
            invitationToken,
            associationId,
            ct);

        return invitation.Id.Value;
    }
}
