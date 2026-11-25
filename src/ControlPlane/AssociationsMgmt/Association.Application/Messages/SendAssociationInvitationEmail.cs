namespace Association.Application.Messages;

public sealed record SendAssociationInvitationEmail(
    string Email,
    string Token,
    Guid AssociationId);