using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.AssociationPorts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using System.Security.Claims;

namespace Association.Application.Services;

public sealed class AssociationClaimsProvider(IQuerySender querySender) : IAssociationUserClaims
{
    private readonly IQuerySender _querySender = querySender;

    public Task<IReadOnlyCollection<Claim>> GetUserAssociationClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        return _querySender.Send(new GetUserAssociationClaimsQuery(userId), ct);
    }
}