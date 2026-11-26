namespace Hivify.Api.Security.PlatformAuth;

public interface IPlatformAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}
