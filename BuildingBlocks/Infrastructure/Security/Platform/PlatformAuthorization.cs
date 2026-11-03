using BuildingBlocks.ApplicationPorts.Security.Platform;

namespace BuildingBlocks.Infrastructure.Security.Platform;

public sealed class PlatformAuthorization(ICurrentPlatformUser currentUser) : IPlatformAuthorization
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
