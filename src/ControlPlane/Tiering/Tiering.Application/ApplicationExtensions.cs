using Microsoft.Extensions.DependencyInjection;
using Tiering.Application.Contracts;
using Tiering.Application.Services;

namespace Tiering.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddTieringServices()
            {

                services.AddScoped<ITierService, TierService>();
                return services;
            }


        }
    }
}
