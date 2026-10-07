using Identity.Application.Contracts;
using Identity.Application.DTOs;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.DotNETIdentity;

public sealed class IdentityService : IUserIdentityService
{
    private readonly UserManager<DotNETApplicationUser> _userManager;
    private readonly SignInManager<DotNETApplicationUser> _signInManager;

    public IdentityService(
        UserManager<DotNETApplicationUser> userManager,
        SignInManager<DotNETApplicationUser> signInManager)
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
        var user = new DotNETApplicationUser
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