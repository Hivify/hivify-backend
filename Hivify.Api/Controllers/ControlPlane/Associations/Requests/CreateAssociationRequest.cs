namespace Hivify.Api.Controllers.ControlPlane.Associations.Requests;

public sealed record CreateAssociationRequest(
    string Name,
    Guid OwnerUserId,
    string OwnerEmailAddress);