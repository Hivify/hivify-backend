using Complaints.Application.Contracts;
using Complaints.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Complaints.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddComplaintsInfrastructure(string connectionString)
            {
                // Database
                services.AddDbContextFactory<ComplaintDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });

                // Infrastructure
                services.AddScoped<IComplaintRepo, ComplaintRepo>();

                return services;
            }
        }
    }
}
