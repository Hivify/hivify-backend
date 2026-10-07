using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.DotNETIdentity;

public sealed class IdentityNoOpEmailSender : IEmailSender<DotNETApplicationUser>
{
    public Task SendConfirmationLinkAsync(DotNETApplicationUser user, string email, string confirmationLink) =>
        Task.CompletedTask;

    public Task SendPasswordResetLinkAsync(DotNETApplicationUser user, string email, string resetLink) =>
        Task.CompletedTask;

    public Task SendPasswordResetCodeAsync(DotNETApplicationUser user, string email, string resetCode) =>
        Task.CompletedTask;

    public Task SendEmailAsync(string email, string subject, string htmlMessage) =>
        Task.CompletedTask;
}