using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Tiering.Application.Contracts;
using Tiering.Infrastructure.Persistence;
namespace Tiering.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddTierInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<TieringDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<ITierRepository, TierRepository>();

                return services;
            }
        }
    }
}
