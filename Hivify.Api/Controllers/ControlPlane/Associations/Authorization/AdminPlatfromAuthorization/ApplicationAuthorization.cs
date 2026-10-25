using BuildingBlocks.ApplicationPorts.CurrentHivifyUserPort;
using Hivify.Api.Controllers.ControlPlane.Associations.Authorization.AdminPlatfromAuthorization;

public sealed class ApplicationAuthorization(ICurrentHivifyUser currentUser) : IApplicationAuthorization
{
    public Task EnsureAuthenticatedAsync(CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException(
                "Authentication is required.");

        return Task.CompletedTask;
    }

    public Task EnsureApplicationAdminAsync(CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException(
                "Authentication is required.");

        if (!currentUser.IsInRole("Admin"))
            throw new UnauthorizedAccessException(
                "Only application administrators can perform this operation.");

        return Task.CompletedTask;
    }
}