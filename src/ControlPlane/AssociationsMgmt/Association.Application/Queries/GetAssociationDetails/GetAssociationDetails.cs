using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociationDetails;

public sealed record GetAssociationDetailsQuery(Guid AssociationId) : IQuery<AssociationDetailsOutput?>;