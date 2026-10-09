using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Association.Application.Queries.GetUserAssociations;

public sealed record GetUserAssociationsQuery : IQuery<IReadOnlyList<TenantAssociationOutput>>;