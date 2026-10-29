using System.Security.Claims;

namespace BuildingBlocks.ApplicationPorts.AssociationPorts
{
    public interface IAssociationUserClaims
    {
        Task<IReadOnlyCollection<Claim>> GetUserAssociationClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
