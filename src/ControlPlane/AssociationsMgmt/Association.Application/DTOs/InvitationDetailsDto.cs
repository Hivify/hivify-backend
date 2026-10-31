namespace Association.Application.DTOs;

public sealed record InvitationDetailsDto(
    Guid InvitationId,
    string AssociationName,
    string InvitedByUserName,
    string InvitedByUserEmail,
    DateTime InvitationSentAt,
    DateTime InvitationExpiresAt,
    bool IsValid
);
