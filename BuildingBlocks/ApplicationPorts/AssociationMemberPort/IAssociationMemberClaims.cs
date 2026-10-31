using System.Security.Claims;

namespace BuildingBlocks.ApplicationPorts.AssociationUserPort
{
    public interface IAssociationMemberClaims
    {
        Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
