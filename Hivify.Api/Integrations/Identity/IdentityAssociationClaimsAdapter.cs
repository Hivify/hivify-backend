using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Identity.Application.Contracts;
using System.Security.Claims;

namespace Hivify.Api.Integrations.Identity;

public sealed class IdentityAssociationClaimsAdapter(IQuerySender querySender) : ITenantMemberClaims
{
    public Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(
        Guid userId,
        CancellationToken ct = default)
        => querySender.Send(new GetUserAssociationClaimsQuery(userId), ct);
}