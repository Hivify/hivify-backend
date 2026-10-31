using BuildingBlocks.ApplicationPorts.AssociationUserPort;
using Finbuckle.MultiTenant.Abstractions;
using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Finbuckle.MultiTenant.Extensions;

namespace Hivify.Api.MultiTenancy;

public static class MultiTenantConfiguration
{

    extension(IServiceCollection services)
    {
        public IServiceCollection AddHivifyMultiTenancy()
        {
            services.AddMultiTenant<TenantInfo>()
                .WithHostStrategy("__tenant__.localhost")
                .WithStore<AssociationTenantStore>(ServiceLifetime.Scoped);

            services.AddScoped<ICurrentAssociationMember, CurrentAssociationTenant>();

            return services;
        }

    }

}
