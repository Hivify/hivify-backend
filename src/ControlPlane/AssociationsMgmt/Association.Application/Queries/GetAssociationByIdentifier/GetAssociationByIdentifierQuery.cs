using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociationByIdentifier;

public sealed record GetAssociationByIdentifierQuery(Guid Identifier) : IQuery<TenantAssociationOutput?>;