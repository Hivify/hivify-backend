using BuildingBlocks.ApplicationPorts.AssociationPorts;
using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Controllers.ControlPlane.Associations.Authorization.TenantRoleAssociationAuthorization
{
    public sealed class AssociationRoleAuthorizationHandler(
        ICurrentAssociationAsTenant currentAssociationTenant) : AuthorizationHandler<AssociationRoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            AssociationRoleRequirement requirement)
        {
            var associationId = currentAssociationTenant.AssociationId;

            var hasRequiredRole = context.User
                .FindAll(AssociationMembershipClaimTypes.TenantMembership)
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
