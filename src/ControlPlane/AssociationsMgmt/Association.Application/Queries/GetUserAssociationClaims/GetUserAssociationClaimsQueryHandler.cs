using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.AssociationUserPort;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using SharedKernel.ValueObjects;
using System.Security.Claims;

namespace Association.Application.Queries.GetUserAssociationClaims;

public sealed class GetUserAssociationClaimsQueryHandler(IAssociationRepository associationRepository)
    : IQueryHandler<GetUserAssociationClaimsQuery, IReadOnlyCollection<Claim>>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;

    public async Task<IReadOnlyCollection<Claim>> Handle(GetUserAssociationClaimsQuery query, CancellationToken ct)
    {
        var userId = new UserID(query.UserId);

        var associations = await _associationRepository.GetByUserIdAsync(userId, ct);

        var claims = new List<Claim>();

        foreach (var association in associations)
        {
            var membership = association.Members.FirstOrDefault(m => m.UserID.Value == query.UserId);

            if (membership is null)
                continue;

            var value = $"{association.Id.Value:D}|{membership.Role}";

            claims.Add(
                new Claim(AssociationMembershipClaimTypes.AssociationMembership, value)
                );
        }

        return claims;
    }
}