using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetUserAssociations;




public sealed record GetAssociationsForUserQuery(Guid UserId) : IQuery<IReadOnlyList<AssciationMemberDto>>;