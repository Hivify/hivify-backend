using System.Security.Claims;

namespace Identity.Application.Contracts;

public interface ITenantClaimsService
{
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}