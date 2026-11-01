using BuildingBlocks.ApplicationPorts.EmailSender;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Infrastructure.EmailSender;

public sealed class DevelopmentEmailSender(
    ILogger<DevelopmentEmailSender> logger) : IEmailSender
{
    public Task SendAsync(
        SentEmailMessage message,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation(
            """
            EMAIL DELIVERY DISABLED (DEVELOPMENT)
            Recipient: {Recipient}
            Subject: {Subject}
            Content:
            {Content}
            """,
            message.To,
            message.Subject,
            message.TextBody);

        return Task.CompletedTask;
    }
}