using Association.Application.Contracts;
using Association.Infrastructure.Email;
using Association.Infrastructure.Persistence;
using BuildingBlocks.ApplicationPorts.Messaging;
using BuildingBlocks.Infrastructure.Messaging.Wolverine;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wolverine.EntityFrameworkCore;

namespace Association.Infrastructure;

public static class InfrastructureExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAssociationInfrastructure(string connectionString)
        {
            // 1. Register AssociationDbContext with Wolverine's EF integration
            services.AddDbContextWithWolverineIntegration<AssociationDbContext>(
                options =>
                {
                    options.UseSqlServer(connectionString);
                });



            // 3. Register the Association outbox
            services.AddScoped<IOutbox, WolverineDbContextOutbox<AssociationDbContext>>();

            // 4. Repositories
            services.AddScoped<
                IAssociationRepository,
                AssociationRepository>();

            services.AddScoped<
                IAssociationInvitationRepository,
                AssociationInvitationRepository>();

            // 5. Email
            services.AddScoped<
                IAssociationInvitationEmailSender,
                AssociationInvitationEmailSender>();

            return services;
        }
    }
}