
namespace Association.Application.Contracts;

public interface IAssociationInvitationEmailSender
{
    Task SendInvitationAsync(
        string recipientEmail,
        string invitationToken,
        Guid associationId,
        CancellationToken ct = default);
}
