using Feeds.Application.Contracts;
using Feeds.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Feeds.Infrastructure
{
    public static class InfrastructureServices
    {

        extension(IServiceCollection services)
        {
            public IServiceCollection AddFeedInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<FeedDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<IFeedRepo, FeedRepo>();

                return services;
            }
        }
    }
}
