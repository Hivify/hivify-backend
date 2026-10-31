using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.AssociationServicesPorts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using System.Security.Claims;

namespace Association.Application.Services;

public sealed class AssociationClaimsAdapter(IQuerySender querySender) : IAssociationMemberClaims
{
    private readonly IQuerySender _querySender = querySender;

    public Task<IReadOnlyCollection<Claim>> GetAssociationMemberClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        return _querySender.Send(new GetUserAssociationClaimsQuery(userId), ct);
    }
}