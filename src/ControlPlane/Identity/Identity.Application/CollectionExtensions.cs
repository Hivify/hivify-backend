using Identity.Application.Contracts;
using Identity.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserMgmtServices()
        {
            services.AddScoped<IUserIdentityService, IdentityService>();
            return services;
        }
    }
}