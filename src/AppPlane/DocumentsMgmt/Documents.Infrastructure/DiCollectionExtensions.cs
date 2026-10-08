using BuildingBlocks.ApplicationPorts.Storage;
using Microsoft.Extensions.DependencyInjection;
namespace DocumentsMgmt.Infrastructure;

public static class DiCollectionExtensions
{
    public static IServiceCollection AddStorageServices(this IServiceCollection services)
    {


        services.AddSingleton<IFileStorage, CloudinaryFileStorage>();

        return services;
    }
}