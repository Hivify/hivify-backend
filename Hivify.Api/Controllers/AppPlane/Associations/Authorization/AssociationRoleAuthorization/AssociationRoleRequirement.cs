using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.AppPlane.Associations.Authorization.AssociationRoleAuthorization
{

    public sealed record AssociationRoleRequirement(string Role) : IAuthorizationRequirement;
}
