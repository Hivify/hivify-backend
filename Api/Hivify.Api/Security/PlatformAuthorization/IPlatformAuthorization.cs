namespace Hivify.Api.Security.PlatformAuthorization;

public interface IPlatformAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}
