namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization;

public interface IApplicationAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}