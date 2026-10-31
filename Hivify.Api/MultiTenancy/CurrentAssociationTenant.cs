using BuildingBlocks.ApplicationPorts.AssociationUserPort;
using Finbuckle.MultiTenant.Abstractions;
using System.Security.Claims;

namespace Hivify.Api.MultiTenancy;

public sealed class CurrentAssociationTenant(
    IMultiTenantContextAccessor<TenantInfo> accessor,
    IHttpContextAccessor httpContextAccessor)
    : ICurrentAssociationMember
{
    private ClaimsPrincipal? User =>
        httpContextAccessor.HttpContext?.User;

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

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated == true;

    public Guid? UserId
    {
        get
        {
            var value = User?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User?.FindFirstValue("sub");

            return Guid.TryParse(value, out var userId)
                ? userId
                : null;
        }
    }

    public string? AssociationMemberRole
    {
        get
        {
            if (AssociationId is not Guid associationId || User is null)
            {
                return null;
            }

            foreach (var claim in User.FindAll(
                AssociationMembershipClaimTypes.AssociationMembership))
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
            AssociationMemberRole,
            "TenantAdmin",
            StringComparison.OrdinalIgnoreCase);

}