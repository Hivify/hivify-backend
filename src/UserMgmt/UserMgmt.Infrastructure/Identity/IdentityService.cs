using Microsoft.AspNetCore.Identity;
using UserMgmt.Application.Contracts;
using UserMgmt.Application.DTOs;

namespace UserMgmt.Infrastructure.Identity;

public sealed class IdentityService : IUserIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<RegisterUserResult> RegisterAsync(
        string email,
        string password,
        string fullName,
        CancellationToken cancellationToken)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            FullName = fullName
        };

        var result = await _userManager.CreateAsync(
            user,
            password);

        return new RegisterUserResult(
            user.Id,
            result.Succeeded,
            result.Errors
                .Select(x => x.Description)
                .ToList());
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