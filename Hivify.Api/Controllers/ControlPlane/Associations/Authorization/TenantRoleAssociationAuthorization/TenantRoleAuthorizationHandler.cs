using BuildingBlocks.ApplicationPorts.AssociationPorts;
using BuildingBlocks.ApplicationPorts.CurrentTenantPort;
using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.TenantRoleAssociationAuthorization
{
    public sealed class TenantRoleAuthorizationHandler(
        ICurrentTenant currentTenant) : AuthorizationHandler<TenantRoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TenantRoleRequirement requirement)
        {
            var associationId = currentTenant.AssociationId;

            var hasRequiredRole = context.User
                .FindAll(HivifyClaimTypes.TenantMembership)
                .Any(claim =>
                {
                    var parts = claim.Value.Split('|');

                    return parts.Length == 2
                        && Guid.TryParse(parts[0], out var claimAssociationId)
                        && claimAssociationId == associationId
                        && string.Equals(
                            parts[1],
                            requirement.Role,
                            StringComparison.OrdinalIgnoreCase);
                });

            if (hasRequiredRole)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
