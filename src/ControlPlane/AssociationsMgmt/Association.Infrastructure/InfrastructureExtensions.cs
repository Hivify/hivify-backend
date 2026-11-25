using Association.Application.Contracts;
using Association.Infrastructure.Email;
using Association.Infrastructure.Persistence;
using BuildingBlocks.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Wolverine;
using Wolverine.Persistence.Durability;
using Wolverine.SqlServer;

namespace Association.Infrastructure;

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

            // Association-specific Wolverine Outbox storage
            services.ConfigureWolverine(opts =>
            {
                opts.PersistMessagesWithSqlServer(connectionString, "association_wolverine",
                    MessageStoreRole.Ancillary)
                    .Enroll<AssociationDbContext>();
            });

            // Outbox
            services.AddScoped<WolverineDbContextOutbox<AssociationDbContext>>();

            services.AddScoped<IAssociationOutbox, AssociationOutbox>();

            // Repositories
            services.AddScoped<IAssociationRepository, AssociationRepository>();

            services.AddScoped<IAssociationInvitationRepository, AssociationInvitationRepository>();

            // Email
            services.AddScoped<IAssociationInvitationEmailSender, AssociationInvitationEmailSender>();

            return services;
        }
    }
}