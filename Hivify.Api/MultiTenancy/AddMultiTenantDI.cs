using Finbuckle.MultiTenant.AspNetCore.Extensions;
using Finbuckle.MultiTenant.Extensions;

namespace Hivify.Api.MultiTenancy;

public static class AddMultiTenantDI
{

    extension(IServiceCollection services)
    {
        public IServiceCollection AddHivifyMultiTenancy()
        {
            services.AddMultiTenant<ExtendedTenantInfo>()
                .WithHostStrategy("__tenant__.localhost")
                .WithStore<AssociationTenantStore>(ServiceLifetime.Scoped);


            return services;
        }

    }

}
