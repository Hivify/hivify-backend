namespace BuildingBlocks.ApplicationPorts.Security.Platform;

public interface IPlatformAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}
