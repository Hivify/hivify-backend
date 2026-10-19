using System.Security.Claims;

namespace BuildingBlocks.ApplicationPorts.Association
{
    public interface IAssociationClaimsProvider
    {
        Task<IReadOnlyCollection<Claim>> GetUserAssociationClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
