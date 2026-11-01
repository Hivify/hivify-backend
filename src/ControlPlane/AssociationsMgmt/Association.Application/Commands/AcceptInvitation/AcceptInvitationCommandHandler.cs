using Association.Application.Contracts;
using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.HivifyUser;
using SharedKernel.ValueObjects;

namespace Association.Application.Commands.AcceptInvitation;

public sealed class AcceptInvitationCommandHandler(
    ICurrentHivifyUser currentUser,
    IAssociationRepository associationRepository,
    IUserSignInRefresher signInRefresher,
    IAssociationInvitationRepository invitationRepository)
    : ICommandHandler<AcceptInvitationCommand, Guid>
{
    public async Task<Guid> Handle(
        AcceptInvitationCommand command,
        CancellationToken ct)
    {
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException(
                "User must be authenticated to accept an invitation.");

        if (string.IsNullOrWhiteSpace(command.Token))
            throw new ArgumentException("Invitation token is required.");

        // 1. Retrieve invitation using the hash of the raw token.
        var tokenHash = InvitationTokenHash.Create(command.Token);

        var invitation = await invitationRepository
            .GetByTokenHashAsync(tokenHash, ct) ??
            throw new InvalidOperationException("Invitation not found.");

        // 2. Validate invitation.
        if (!invitation.IsValid(DateTimeOffset.UtcNow))
            throw new InvalidOperationException("Invitation is invalid or expired.");

        // 3. Verify authenticated user's email.
        if (!string.Equals(
                currentUser.EmailAddress,
                invitation.Email.Value,
                StringComparison.OrdinalIgnoreCase))
        {
            throw new UnauthorizedAccessException(
                "Your email address does not match the invitation.");
        }

        // 4. Retrieve the Association.
        var association = await associationRepository.GetByIdAsync(invitation.AssociationID, ct) ??
            throw new InvalidOperationException("Association not found.");

        // 5. Add authenticated user as a member.
        association.AddMember(
            new UserID(currentUser.UserId),
            AssociationRoles.AssociationMember,
            new ContactEmail(currentUser.EmailAddress));

        // 6. Accept invitation.
        invitation.Accept();

        // 7. Persist both changes.
        await associationRepository.SaveChangesAsync(ct);

        await signInRefresher.RefreshAsync();

        return association.Id.Value;
    }
}