using Resend;

namespace BuildingBlocks.ApplicationPorts.Email;

public interface IEmailSender
{
    Task SendAsync(
        ConfirmationMessage message,
        CancellationToken ct = default);
}