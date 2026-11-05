using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.AppPlane.Authorization.TenantRoleAuthorization
{

    public sealed record TenantRoleRequirement(string Role) : IAuthorizationRequirement;
}
