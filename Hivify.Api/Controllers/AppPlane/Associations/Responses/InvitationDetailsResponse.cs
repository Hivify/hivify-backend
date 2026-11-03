namespace Hivify.Api.Controllers.AppPlane.Associations.Responses;

public sealed record InvitationDetailsResponse(
    Guid InvitationId,
    string AssociationName,
    string Email,
    string Status,
    DateTimeOffset CreatedAt,
    DateTimeOffset ExpiresAt,
    bool IsValid);