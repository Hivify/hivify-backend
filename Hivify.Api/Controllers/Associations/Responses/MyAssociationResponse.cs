namespace Hivify.Api.Controllers.Associations.Responses;

public sealed record MyAssociationResponse(
    Guid AssociationId,
    string Name,
    Guid Identifier,
    string Role);