using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAllAssociations
{
    public sealed record GetAllAssociationsQuery : IQuery<IReadOnlyList<AssociationDto>>;
}
