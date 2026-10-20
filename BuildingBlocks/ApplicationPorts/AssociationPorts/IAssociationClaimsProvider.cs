using System.Security.Claims;

namespace BuildingBlocks.ApplicationPorts.AssociationPorts
{
    public interface IAssociationClaimsProvider
    {
        Task<IReadOnlyCollection<Claim>> GetUserAssociationClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
