using BuildingBlocks.ApplicationPorts.Security.Tenant;
using BuildingBlocks.Infrastructure.AssociationAsTenant;
using Finbuckle.MultiTenant.Abstractions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace BuildingBlocks.Infrastructure.Security.Tenant;

public sealed class CurrentTenantUser(
    IMultiTenantContextAccessor<TenantInfo> accessor,
    IHttpContextAccessor httpContextAccessor)
    : ICurrentTenantUser
{
    private ClaimsPrincipal? User =>
        httpContextAccessor.HttpContext?.User;

    public Guid UserId
    {
        get
        {
            var value = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User?.FindFirstValue("sub");

            if (!Guid.TryParse(value, out var userId) || userId == Guid.Empty)
                throw new UnauthorizedAccessException(
                    "The current user does not have a valid user ID.");

            return userId;
        }
    }

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public string EmailAddress =>
        User?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;

    public Guid? AssociationId
    {
        get
        {
            var tenantInfo = accessor.MultiTenantContext?.TenantInfo;

            if (tenantInfo is null ||
                !Guid.TryParse(tenantInfo.Id, out var associationId) ||
                associationId == Guid.Empty)
            {
                return null;
            }

            return associationId;
        }
    }

    public string? AssociationRole
    {
        get
        {
            if (AssociationId is not Guid associationId || User is null)
            {
                return null;
            }

            foreach (var claim in User.FindAll(AssociationMemberClaimTypes.AssociationMembership))
            {
                var parts = claim.Value.Split('|');

                if (parts.Length == 2 &&
                    Guid.TryParse(parts[0], out var claimAssociationId) &&
                    claimAssociationId == associationId)
                {
                    return parts[1];
                }
            }

            return null;
        }
    }

    public bool IsAssociationAdmin =>
        string.Equals(
            AssociationRole,
            "AssociationAdmin",
            StringComparison.OrdinalIgnoreCase);
}
