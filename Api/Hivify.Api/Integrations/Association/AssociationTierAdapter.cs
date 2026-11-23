using Association.Application.Contracts;
using Association.Application.DTOs;
using Tiering.Application.Contracts;

namespace Hivify.Api.Integrations.Association
{
    public sealed class AssociationTierAdapter(ITierService tierService) : IAssociationTierPort
    {
        public async Task<AssociationTierPolicy?> GetPolicyAsync(
            Guid tierId,
            CancellationToken ct = default)
        {
            var policy = await tierService.GetTierPolicyAsync(tierId, ct);

            if (policy is null)
                return null;

            return new AssociationTierPolicy(
                policy.MaxMembers,
                policy.MaxAdministrators,
                policy.Features);
        }
    }
}
