using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetMyAssociations;

public sealed record GetUserAssociationsQuery : IQuery<IReadOnlyList<UserAssociationListItem>>;