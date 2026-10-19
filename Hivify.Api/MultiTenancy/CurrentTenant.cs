using BuildingBlocks.ApplicationPorts.CurrentTenantProvider;
using Finbuckle.MultiTenant.Abstractions;

namespace Hivify.Api.MultiTenancy
{
    public sealed class CurrentTenant(IMultiTenantContextAccessor<TenantInfo> accessor) : ICurrentTenant
    {
        public Guid AssociationId =>
            Guid.Parse(accessor.MultiTenantContext.TenantInfo!.Id);
    }
}
