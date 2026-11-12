using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Identity.Application.Contracts;
using System.Security.Claims;

namespace Hivify.Api.Integrations.Identity;

public sealed class IdentityAssociationClaimsAdapter(IQuerySender querySender) : IAssociationMemberClaims
{
    private readonly IQuerySender _querySender = querySender;

    public Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        return _querySender.Send(new GetUserAssociationClaimsQuery(userId), ct);
    }
}