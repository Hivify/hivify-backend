using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.TenantRoleAssociationAuthorization
{

    public sealed record TenantRoleRequirement(string Role) : IAuthorizationRequirement;
}
