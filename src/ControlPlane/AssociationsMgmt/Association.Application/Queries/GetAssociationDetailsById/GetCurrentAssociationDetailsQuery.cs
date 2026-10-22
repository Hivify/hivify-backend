using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAssociationDetailsById;

public sealed record GetCurrentAssociationDetailsQuery() : IQuery<AssociationDto?>;