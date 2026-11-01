using Association.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Association.Infrastructure.Email;

public sealed class DevelopmentAssociationInvitationEmailSender(
    ILogger<DevelopmentAssociationInvitationEmailSender> logger)
    : IAssociationInvitationEmailSender
{
    public Task SendInvitationAsync(
        string recipientEmail,
        string invitationToken,
        Guid associationId,
        CancellationToken ct = default)
    {
        var invitationUrl =
            $"http://localhost:3000/accept-invitation" +
            $"?token={Uri.EscapeDataString(invitationToken)}";

        logger.LogInformation(
            """
            DEVELOPMENT INVITATION EMAIL
            Recipient: {Recipient}
            Association ID: {AssociationId}
            Invitation URL: {InvitationUrl}
            """,
            recipientEmail,
            associationId,
            invitationUrl);

        return Task.CompletedTask;
    }
}