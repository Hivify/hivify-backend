using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Security.Tenant.TenantAuthorization
{
    public sealed class TenantRoleAuthorizationHandler(
        ICurrentTenantUser currentAssociationTenant) : AuthorizationHandler<TenantRoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TenantRoleRequirement requirement)
        {
            var associationId = currentAssociationTenant.AssociationId;

            var hasRequiredRole = context.User
                .FindAll(TenantUserClaimTypes.AssociationMembership)
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
