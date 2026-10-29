using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using System.Security.Claims;

namespace Association.Application.Queries.GetUserAssociationClaims;

public sealed record GetUserAssociationClaimsQuery(Guid UserId) : IQuery<IReadOnlyCollection<Claim>>;