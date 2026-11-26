using Hivify.Api.Security.PlatformAuth;

namespace Hivify.Api.Security;

public static class SecurityExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddSecurityExtensions()
        {
            // Security
            services.AddScoped<IPlatformAuthorization, PlatformAuthorization>();
            return services;
        }
    }
}

