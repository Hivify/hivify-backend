namespace Hivify.Api.Controllers.AppPlane.Associations.Responses;

public sealed record AssociationMemberResponse(
    Guid UserId,
    string Role,
    DateTimeOffset JoinedAt);