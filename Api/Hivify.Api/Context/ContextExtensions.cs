using BuildingBlocks.ApplicationPorts.Context.Platform;
using BuildingBlocks.ApplicationPorts.Context.Tenant;
using Hivify.Api.Context.Platform;
using Hivify.Api.Context.Tenant;

namespace Hivify.Api.Context;

public static class ContextExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddContextExtensions()
        {



            // Security
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentPlatformUser, CurrentPlatformUser>();
            services.AddScoped<ICurrentTenantUser, CurrentTenantUser>();
            services.AddScoped<ICurrentTenant, CurrentTenant>();


            return services;
        }
    }
}

