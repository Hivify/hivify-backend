using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnBoarding.Application.Contracts;
using OnBoarding.Infrastructure.Persistence;
namespace OnBoarding.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<OnboardingDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<IOnboardingRepository, OnboardingRepository>();

                return services;
            }
        }
    }
}
