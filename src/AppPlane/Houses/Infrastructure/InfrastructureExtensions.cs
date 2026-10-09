using Houses.Application.Contracts;
using Houses.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Houses.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddHousesInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<HouseDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<IHouseRepo, HouseRepo>();

                return services;
            }
        }
    }
}

