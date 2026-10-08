using Department.Application.Contracts;
using Department.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Department.Infrastructure;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddDepartmentInfrastructure(this IServiceCollection services, string connectionString)
    {
        // Database
        services.AddDbContextFactory<DepartmentDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        // Infrastructure
        services.AddScoped<IDepartmentRepo, DepartmentRepo>();

        return services;
    }
}
