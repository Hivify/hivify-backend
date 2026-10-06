namespace Hivify.Api.Controllers.Associations.Responses;

public sealed record MyAssociationResponse(
    Guid AssociationId,
    string Name,
    string Identifier,
    string Role);