using BuildingBlocks.ApplicationPorts.Email;
using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace Identity.Application.Services;

public sealed class IdentityService(
    UserManager<DotNETApplicationUser> userManager,
    SignInManager<DotNETApplicationUser> signInManager,
    IConfiguration configuration,
    IEmailSender emailSender) : IUserIdentityService
{
    private readonly UserManager<DotNETApplicationUser> _userManager = userManager;
    private readonly SignInManager<DotNETApplicationUser> _signInManager = signInManager;
    private readonly IConfiguration _configuration = configuration;
    private readonly IEmailSender _emailSender = emailSender;

    public async Task<RegisterUserResult> RegisterAsync(
        string email,
        string password,
        string fullName,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var user = new DotNETApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            FullName = fullName,
            EmailConfirmed = false
        };

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            return new RegisterUserResult(
                user.Id,
                false,
                [.. result.Errors.Select(x => x.Description)]);
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var confirmationBaseUrl =
            _configuration["Frontend:EmailConfirmationUrl"]
            ?? throw new InvalidOperationException(
                "Frontend:EmailConfirmationUrl is not configured.");

        var confirmationLink =
            $"{confirmationBaseUrl}?userId={user.Id}" +
            $"&token={Uri.EscapeDataString(encodedToken)}";

        var confirmationMessage = new ConfirmationMessage(
            user.Email!,
            "Confirm your email",
            $"<p>Please confirm your email by clicking <a href=\"{confirmationLink}\">here</a>.</p>"
        );

        await _emailSender.SendAsync(confirmationMessage, cancellationToken);

        return new RegisterUserResult(
            user.Id,
            true,
            []);
    }

    public async Task<LoginUserResult> LoginAsync(
        string email,
        string password,
        bool rememberMe,
        CancellationToken cancellationToken)
    {
        var result =
            await _signInManager.PasswordSignInAsync(
                email,
                password,
                rememberMe,
                lockoutOnFailure: true);

        return new LoginUserResult(
            result.Succeeded,
            result.IsLockedOut,
            result.RequiresTwoFactor);
    }

    public async Task LogoutAsync(
        CancellationToken cancellationToken)
    {
        await _signInManager.SignOutAsync();
    }
}