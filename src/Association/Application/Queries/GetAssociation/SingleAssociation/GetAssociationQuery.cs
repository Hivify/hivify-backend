using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociation.SingleAssociation;

public sealed record GetAssociationQuery(Guid AssociationId) : IQuery<AssociationListItem>;


