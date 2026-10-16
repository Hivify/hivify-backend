namespace Hivify.Api.Controllers.ControlPlane.Associations.Responses;

public sealed record MyAssociationResponse(
    Guid AssociationId,
    string Name,
    Guid Identifier
);