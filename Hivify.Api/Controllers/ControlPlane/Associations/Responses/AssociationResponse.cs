namespace Hivify.Api.Controllers.ControlPlane.Associations.Responses;

public sealed record AssociationResponse(
    Guid AssociationId,
    string AssociationName,
    string AdminAssociationEmail,
    Guid Identifier,
    string Status,
    DateTime CreatedDate
);
