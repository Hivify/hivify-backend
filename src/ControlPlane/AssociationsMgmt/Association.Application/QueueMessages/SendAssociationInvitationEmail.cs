namespace Association.Application.QueueMessages;

public sealed record SendAssociationInvitationEmail(
    string Email,
    string Token,
    Guid AssociationId);