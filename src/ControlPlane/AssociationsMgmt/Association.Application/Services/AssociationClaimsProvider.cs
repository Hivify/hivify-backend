using Association.Application.Queries.GetUserAssociationClaims;
using BuildingBlocks.ApplicationPorts.Association;
using BuildingBlocks.ApplicationPorts.Messaging;
using System.Security.Claims;

namespace Association.Application.Services;

public sealed class AssociationClaimsProvider(IQuerySender querySender) : IAssociationClaimsProvider
{
    private readonly IQuerySender _querySender = querySender;

    public Task<IReadOnlyCollection<Claim>> GetUserAssociationClaimsAsync(Guid userId, CancellationToken ct = default)
    {
        return _querySender.Send(
            new GetUserAssociationClaimsQuery(userId),
            ct);
    }
}