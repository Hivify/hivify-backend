using System.Security.Claims;

namespace BuildingBlocks.ApplicationPorts.AssociationServicesPorts
{
    public interface IAssociationMemberClaims
    {
        Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
