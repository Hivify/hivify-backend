using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using BuildingBlocks.ApplicationPorts.Contracts.Storage;
using BuildingBlocks.Infrastructure.CurrentUserProvider;
using BuildingBlocks.Infrastructure.Messaging;
using BuildingBlocks.Infrastructure.Storage.CloudinaryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuildingBlocks.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddBuildingBlocks(IConfiguration configuration)
            {
                // Messaging 
                services.AddScoped<ISender, Sender>();
                services.AddScoped<IQuerySender, QuerySender>();

                // Storage
                services.Configure<CloudinaryOptions>(
                    configuration.GetSection("Cloudinary"));

                services.AddScoped<IFileStorage, CloudinaryFileStorage>();

                // current User
                services.AddHttpContextAccessor();
                services.AddScoped<ICurrentUser, CurrentUser>();
                return services;
            }
        }
    }
}