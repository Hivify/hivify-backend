using BuildingBlocks.ApplicationPorts.Context.Tenant;
using Microsoft.AspNetCore.Authorization;

namespace Hivify.Api.Security.TenantAuth
{
    public sealed class TenantRoleAuthorizationHandler(ICurrentTenantUser currentTenantUser) : AuthorizationHandler<TenantRoleRequirement>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            TenantRoleRequirement requirement)
        {
            var associationId = currentTenantUser.TenantId;

            var hasRequiredRole = context.User
                .FindAll(TenantUserClaimTypes.TenantMembership)
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
