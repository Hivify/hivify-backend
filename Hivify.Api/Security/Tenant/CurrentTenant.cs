
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Finbuckle.MultiTenant.Abstractions;
using Hivify.Api.MultiTenancy;

namespace Hivify.Api.Security.Tenant;

public sealed class CurrentTenant(
    IMultiTenantContextAccessor<ExtendedTenantInfo> accessor) : ICurrentTenant
{
    private ExtendedTenantInfo? TenantInfo =>
        accessor.MultiTenantContext?.TenantInfo;

    public Guid? AssociationId
    {
        get
        {
            if (!Guid.TryParse(TenantInfo?.Id, out var associationId) ||
                associationId == Guid.Empty)
            {
                return null;
            }

            return associationId;
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

    public string? AssociationName =>
        TenantInfo?.Name;
}
