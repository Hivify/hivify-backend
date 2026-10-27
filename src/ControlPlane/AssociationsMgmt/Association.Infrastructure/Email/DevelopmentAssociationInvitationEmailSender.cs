using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.EmailSender;

namespace Association.Infrastructure.Email;

public sealed class ResendAssociationInvitationEmailSender(
    IEmailSender emailSender)
    : IAssociationInvitationEmailSender
{
    private readonly IEmailSender _emailSender = emailSender;

    public async Task SendInvitationAsync(
        string recipientEmail,
        string invitationToken,
        Guid associationId,
        CancellationToken ct = default)
    {
        var invitationUrl =
            $"http://localhost:3000/accept-invitation" +
            $"?token={Uri.EscapeDataString(invitationToken)}";

        var message = new SentEmailMessage(
            To: recipientEmail,
            Subject: "You're invited to join Hivify",
            HtmlBody: $"""
                <h2>You've been invited to join Hivify</h2>
                <p>You've received an invitation to join an association on Hivify.</p>
                <p>
                    <a href="{invitationUrl}">Accept invitation</a>
                </p>
                <p>This invitation expires in 7 days.</p>
                """,
            TextBody:
                $"You've been invited to join Hivify. " +
                $"Accept your invitation here: {invitationUrl}");

        await _emailSender.SendAsync(message, ct);
    }
}