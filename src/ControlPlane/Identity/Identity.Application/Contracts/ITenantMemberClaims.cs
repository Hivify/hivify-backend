using System.Security.Claims;

namespace Identity.Application.Contracts
{
    public interface ITenantMemberClaims
    {
        Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
