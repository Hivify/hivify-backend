using Association.Application.Contracts;
using Association.Application.Messages;

namespace Association.Infrastructure.Messaging;

public sealed class SendAssociationInvitationEmailHandler(IAssociationInvitationEmailSender emailSender)
{
    public Task Handle(SendAssociationInvitationEmail message, CancellationToken ct)
    {
        return emailSender.SendInvitationAsync(
            message.Email,
            message.Token,
            message.AssociationId,
            ct);
    }
}