using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAssociationByIdentifier;

public sealed record GetAssociationByIdentifierQuery(Guid Identifier) : IQuery<AssociationDto?>;