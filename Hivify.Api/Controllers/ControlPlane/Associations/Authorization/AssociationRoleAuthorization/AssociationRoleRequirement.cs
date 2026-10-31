using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.AssociationRoleAuthorization
{

    public sealed record AssociationRoleRequirement(string Role) : IAuthorizationRequirement;
}
