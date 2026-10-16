using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAllAssociations
{
    public sealed record GetAllAssociationsQuery : IQuery<IReadOnlyList<TenantAssociationOutput>>;
}
