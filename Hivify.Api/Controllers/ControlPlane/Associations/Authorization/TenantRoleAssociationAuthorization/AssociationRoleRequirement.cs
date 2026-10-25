using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.TenantRoleAssociationAuthorization
{

    public sealed record AssociationRoleRequirement(string Role) : IAuthorizationRequirement;
}
