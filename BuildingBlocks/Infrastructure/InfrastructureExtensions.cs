using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.EmailSender;
using BuildingBlocks.ApplicationPorts.Storage;
using BuildingBlocks.Infrastructure.CQRSMessaging;
using BuildingBlocks.Infrastructure.EmailSender;
using BuildingBlocks.Infrastructure.Storage.CloudinaryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;

namespace BuildingBlocks.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddBuildingBlocks(IConfiguration configuration, bool isDevelopment)
            {

                // Messaging 
                services.AddScoped<ICommandSender, CommandSender>();
                services.AddScoped<IQuerySender, QuerySender>();

                // Storage
                services.Configure<CloudinaryOptions>(configuration.GetSection("Cloudinary"));

                services.AddScoped<IFileStorage, CloudinaryFileStorage>();


                // Email
                services.AddResend(options =>
                {
                    options.ApiToken = configuration["Resend:ApiKey"]
                        ?? throw new InvalidOperationException(
                            "Resend:ApiKey is not configured.");
                });

                if (isDevelopment)
                {
                    services.AddScoped<IEmailSender, DevelopmentEmailSender>();
                }
                else
                {
                    services.AddScoped<IEmailSender, ResendEmailSender>();
                }

                return services;
            }
        }
    }
}