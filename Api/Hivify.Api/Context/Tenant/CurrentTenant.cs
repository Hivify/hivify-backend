using BuildingBlocks.ApplicationPorts.Context.Tenant;
using Finbuckle.MultiTenant.Abstractions;
using Hivify.Api.MultiTenancy;

namespace Hivify.Api.Context.Tenant;

public sealed class CurrentTenant(IMultiTenantContextAccessor<ExtendedTenantInfo> accessor) : ICurrentTenant
{
    private ExtendedTenantInfo? TenantInfo =>
        accessor.MultiTenantContext?.TenantInfo;

    public Guid? TenantId
    {
        get
        {
            if (!Guid.TryParse(TenantInfo?.Id, out var tenantId) ||
                tenantId == Guid.Empty)
            {
                return null;
            }

            return tenantId;
        }
    }

    public Guid? TierId
    {
        get
        {
            if (!Guid.TryParse(TenantInfo?.TierId, out var tierId) ||
                tierId == Guid.Empty)
            {
                return null;
            }

            return tierId;
        }
    }



    public string? Identifier =>
        TenantInfo?.Identifier;

    public string? TenantName =>
        TenantInfo?.Name;
}
