namespace Hivify.Api.Security.Platform.PlatformAuthorization;

public interface IPlatformAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}
