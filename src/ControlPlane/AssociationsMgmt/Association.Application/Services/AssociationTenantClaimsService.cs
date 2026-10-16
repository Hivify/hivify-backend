using Association.Application.Constants;
using Association.Application.Contracts;
using SharedKernel.ValueObjects;
using System.Security.Claims;

namespace Association.Application.Services;

public sealed class AssociationTenantClaimsService(
    IAssociationRepository associationRepository)
    : ITenantClaimsService
{
    public async Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        var associations = await associationRepository.GetByUserIdAsync(
            new UserID(userId),
            cancellationToken);

        var claims = new List<Claim>();

        foreach (var association in associations)
        {
            var membership = association.Members
                .FirstOrDefault(m => m.UserID.Value == userId);

            if (membership is null)
                continue;

            var value =
                $"{association.Id.Value:D}|{membership.Role}";

            claims.Add(new Claim(
                HivifyClaimTypes.TenantMembership,
                value));
        }

        return claims;
    }
}