using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.HivifyUserPort;
using BuildingBlocks.ApplicationPorts.Storage;
using BuildingBlocks.Infrastructure.CQRSMessaging;
using BuildingBlocks.Infrastructure.HivifyUser;
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
                //HivifyCurrentUser
                services.AddHttpContextAccessor();
                services.AddScoped<ICurrentHivifyUser, CurrentHivifyUser>();
                services.AddScoped<IHivifyUserAuthorization, HivifyUserAuthorization>();
                // Messaging 
                services.AddScoped<ISender, CommandSender>();
                services.AddScoped<IQuerySender, QuerySender>();

                // Storage
                services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));

                services.AddScoped<IFileStorage, CloudinaryFileStorage>();






                return services;
            }
        }
    }
}