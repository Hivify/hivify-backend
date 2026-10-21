namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.AdminPlatfromAuthorization;

public interface IApplicationAuthorization
{
    Task EnsureAuthenticatedAsync(CancellationToken ct = default);

    Task EnsureApplicationAdminAsync(CancellationToken ct = default);
}