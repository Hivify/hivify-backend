using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAllAssociationByUser;

public sealed record GetAllAssociationsByUserQuery(Guid UserId)
    : IQuery<IReadOnlyList<UserAssociationDto>>;