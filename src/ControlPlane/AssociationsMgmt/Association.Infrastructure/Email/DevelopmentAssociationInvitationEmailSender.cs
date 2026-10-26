using Association.Application.Contracts;
using Microsoft.Extensions.Logging;

namespace Association.Infrastructure.Email;

public sealed class DevelopmentAssociationInvitationEmailSender(
    ILogger<DevelopmentAssociationInvitationEmailSender> logger) : IAssociationInvitationEmailSender
{
    public Task SendInvitationAsync(
        string recipientEmail,
        string invitationToken,
        Guid associationId,
        CancellationToken ct = default)
    {
        logger.LogInformation(
            "Development invitation for {RecipientEmail}, " +
            "Association {AssociationId}. Token: {InvitationToken}",
            recipientEmail,
            associationId,
            invitationToken);

        return Task.CompletedTask;
    }
}