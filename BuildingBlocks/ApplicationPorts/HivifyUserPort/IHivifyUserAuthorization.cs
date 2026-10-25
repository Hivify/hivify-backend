namespace BuildingBlocks.ApplicationPorts.HivifyUserPort;

public interface IHivifyUserAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}