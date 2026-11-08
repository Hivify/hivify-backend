using System.Security.Claims;

namespace Association.Application.Contracts
{
    public interface IAssociationMemberClaims
    {
        Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default);
    }
}
