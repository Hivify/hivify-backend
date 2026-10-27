using Resend;

namespace BuildingBlocks.ApplicationPorts.EmailSender;

public interface IEmailSender
{
    Task SendAsync(
        SentEmailMessage message,
        CancellationToken ct = default);
}