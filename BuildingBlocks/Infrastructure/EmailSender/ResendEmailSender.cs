using BuildingBlocks.ApplicationPorts.Email;
using Microsoft.Extensions.Configuration;
using Resend;

namespace BuildingBlocks.Infrastructure.Email;

public sealed class ResendEmailSender(
    IResend resend,
    IConfiguration configuration) : IEmailSender
{
    public async Task SendAsync(
        ConfirmationMessage message,
        CancellationToken cancellationToken = default)
    {
        var from = configuration["Resend:FromEmail"]
            ?? throw new InvalidOperationException(
                "Resend:FromEmail is not configured.");

        var email = new Resend.EmailMessage
        {
            From = from,
            Subject = message.Subject,
            HtmlBody = message.HtmlBody,
            TextBody = message.TextBody
        };

        email.To.Add(message.To);

        await resend.EmailSendAsync(email);
    }


}