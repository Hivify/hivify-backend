using BuildingBlocks.ApplicationPorts.UserContext.Platform;
using BuildingBlocks.ApplicationPorts.UserContext.Tenant;
using Hivify.Api.Security.PlatformAuthorization;
using Hivify.Api.UserContext.Platform;
using Hivify.Api.UserContext.Tenant;

namespace Hivify.Api.UserContext;

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
            services.AddScoped<ICurrentTenant, CurrentTenant>();


            return services;
        }
    }
}

