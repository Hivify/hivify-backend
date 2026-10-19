namespace BuildingBlocks.ApplicationPorts.Authorization;

public interface IApplicationAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}