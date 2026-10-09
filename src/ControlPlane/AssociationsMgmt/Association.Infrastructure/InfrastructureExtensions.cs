using Association.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace Association.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<AssociationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<IAssociationRepository, AssociationRepository>();

                return services;
            }
        }
    }
}
