namespace Hivify.Api.Controllers.ControlPlane.Associations.Responses;

public sealed record AssociationResponse(
    Guid AssociationId,
    string Name,
    Guid Identifier,
    string Status,
    DateTime CreatedDate
);
