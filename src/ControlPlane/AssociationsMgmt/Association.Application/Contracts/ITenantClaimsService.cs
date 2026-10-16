using System.Security.Claims;

namespace Association.Application.Contracts;

public interface ITenantClaimsService
{
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}