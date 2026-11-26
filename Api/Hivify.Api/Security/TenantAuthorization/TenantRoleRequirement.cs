using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Security.TenantAuthorization
{

    public sealed record TenantRoleRequirement(string Role) : IAuthorizationRequirement;
}
