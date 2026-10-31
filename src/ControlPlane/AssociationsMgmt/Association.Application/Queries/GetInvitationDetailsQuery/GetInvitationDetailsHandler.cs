using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetInvitationDetailsQuery;

public sealed class GetInvitationDetailsHandler(
    IAssociationInvitationRepository associationInvitationRepository,
    IAssociationRepository associationRepository
) : IQueryHandler<GetInvitationDetailsQuery, InvitationDetailsDto?>
{
    public async Task<InvitationDetailsDto?> Handle(
        GetInvitationDetailsQuery query,
        CancellationToken ct)
    {
        // 1. Validate the original token.
        if (string.IsNullOrWhiteSpace(query.Token))
            return null;

        // 2. Hash the token.
        var tokenHash = InvitationTokenHash.Create(query.Token);

        // 3. Retrieve invitation.
        var invitation = await associationInvitationRepository
            .GetByTokenHashAsync(tokenHash, ct);

        if (invitation is null)
            return null;

        // 4. Retrieve the Association.
        var association = await associationRepository
            .GetByIdAsync(invitation.AssociationID, ct);

        if (association is null)
            return null;

        // 5. Check invitation validity.
        var isValid = invitation.IsValid(DateTime.UtcNow);

        // 6. Return details.
        return new InvitationDetailsDto(
            invitation.Id.Value,
            association.Name.Value,
            invitation.Email.Value,
            invitation.Status.ToString(),
            invitation.Expiration.CreatedAt,
            invitation.Expiration.ExpiresAt,
            isValid
        );
    }
}