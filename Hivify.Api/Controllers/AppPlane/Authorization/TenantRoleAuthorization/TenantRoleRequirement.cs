using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.AppPlane.Authorization
{

    public sealed record TenantRoleRequirement(string Role) : IAuthorizationRequirement;
}
