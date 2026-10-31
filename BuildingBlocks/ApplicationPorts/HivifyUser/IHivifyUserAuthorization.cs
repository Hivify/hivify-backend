namespace BuildingBlocks.ApplicationPorts.HivifyUser;

public interface IHivifyUserAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}