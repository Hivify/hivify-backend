using BuildingBlocks.ApplicationPorts.Security.Platform;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using Hivify.Api.Security.Platform;
using Hivify.Api.Security.Platform.PlatformAuthorization;
using Hivify.Api.Security.Tenant;

namespace Hivify.Api.Security;

public static class AddServicesSecurityDI
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddServicesSecurity()
        {



            // Security
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentPlatformUser, CurrentPlatformUser>();
            services.AddScoped<IPlatformAuthorization, PlatformAuthorization>();
            services.AddScoped<ICurrentTenantUser, CurrentTenantUser>();


            return services;
        }
    }
}

