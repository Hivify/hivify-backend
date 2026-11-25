
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.EmailSender;
using BuildingBlocks.ApplicationPorts.Storage;
using BuildingBlocks.Infrastructure.CQRSMessaging;
using BuildingBlocks.Infrastructure.EmailSender;
using BuildingBlocks.Infrastructure.Storage.CloudinaryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Resend;
using Wolverine;
using Wolverine.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure;

public static class InfrastructureExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddBuildingBlocks(IConfiguration configuration, bool isDevelopment)
        {

            var connectionString = configuration.GetConnectionString("DefaultConnection")
         ?? throw new InvalidOperationException(
             "Connection string 'DefaultConnection' not found.");



            // CQRS
            services.AddScoped<ICommandSender, CommandSender>();
            services.AddScoped<IQuerySender, QuerySender>();

            // Wolverine
            services.ConfigureWolverine(opts =>
            {
                // EF Core transactional messaging support
                opts.UseEntityFrameworkCoreTransactions();

                // Durable local background queues
                opts.Policies.UseDurableLocalQueues();
            });

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


            // Email sender implementation based on the environment

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
