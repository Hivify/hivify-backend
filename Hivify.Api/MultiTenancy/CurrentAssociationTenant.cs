using BuildingBlocks.ApplicationPorts.AssociationPorts;
using Finbuckle.MultiTenant.Abstractions;

namespace Hivify.Api.MultiTenancy;

public sealed class CurrentAssociationTenant(
    IMultiTenantContextAccessor<TenantInfo> accessor) : ICurrentAssociationTenant
{
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
}
