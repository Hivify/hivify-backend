using Identity.Application.DTOs;

namespace Identity.Application.Contracts;


public interface IUserIdentityService
{
    Task<RegisterUserResult> RegisterAsync(
        string email,
        string password,
        string fullName,
        CancellationToken cancellationToken);

    Task<LoginUserResult> LoginAsync(
        string email,
        string password,
        bool rememberMe,
        CancellationToken cancellationToken);

    Task LogoutAsync(
        CancellationToken cancellationToken);
}


