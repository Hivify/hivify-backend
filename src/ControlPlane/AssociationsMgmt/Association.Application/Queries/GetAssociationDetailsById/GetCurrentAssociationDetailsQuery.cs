using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAssociationDetailsById;

public sealed record GetCurrentAssociationDetailsQuery() : IQuery<AssociationDto?>;