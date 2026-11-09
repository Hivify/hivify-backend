using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Security.Tenant.TenantAuthorization
{

    public sealed record TenantRoleRequirement(string Role) : IAuthorizationRequirement;
}
