using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAllAssociationsByUser;

public sealed record GetAllAssociationsByUserQuery(Guid UserId)
    : IQuery<IReadOnlyList<UserAssociationDto>>;