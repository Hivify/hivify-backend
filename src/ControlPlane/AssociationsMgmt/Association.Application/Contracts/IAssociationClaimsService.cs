using System.Security.Claims;

namespace Association.Application.Contracts;

public interface IAssociationClaimsService
{
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}