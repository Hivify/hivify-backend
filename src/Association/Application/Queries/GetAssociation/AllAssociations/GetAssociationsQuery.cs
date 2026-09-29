using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociation.AllAssociations;

public sealed record GetAssociationsQuery : IQuery<IReadOnlyList<AssociationListItem>>;



