namespace Association.Application.DTOs;

public sealed record InvitationDetailsDto(
    Guid InvitationId,
    string AssociationName,
    string InvitedByUserName,
    string InvitedByUserEmail,
    DateTimeOffset InvitationSentAt,
    DateTimeOffset InvitationExpiresAt,
    bool IsValid
);
