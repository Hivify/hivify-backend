using Association.Application.Commands.CreateAssociation;
using Association.Application.DTOs;
using Association.Application.Queries.GetAllAssociations;
using Association.Application.Queries.GetAssociationByIdentifier;
using Association.Application.Queries.GetUserAssociationClaims;
using Association.Application.Queries.GetUserAssociations;
using Association.Application.Services;
using BuildingBlocks.ApplicationPorts.Association;
using BuildingBlocks.ApplicationPorts.Messaging;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Association.Application;

public static class ApplicationExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddAssociationServices()
        {
            services.AddScoped<
                ICommandHandler<CreateAssociationCommand, Guid>,
                CreateAssociationCommandHandler>();

            services.AddScoped<
                IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>,
                GetUserAssociationsQueryHandler>();

            services.AddScoped<
                IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>,
                GetAllAssociationsQueryHandler>();

            services.AddScoped<
                IQueryHandler<GetAssociationByIdentifierQuery, TenantAssociationOutput?>,
                GetAssociationByIdentifierQueryHandler>();

            services.AddScoped<
                IQueryHandler<GetUserAssociationClaimsQuery, IReadOnlyCollection<Claim>>,
                GetUserAssociationClaimsQueryHandler>();

            services.AddScoped<IAssociationClaimsProvider, AssociationClaimsProvider>();

            services.AddScoped<IAssociationOnboardingPort, AssociationOnboardingAdapter>();

            return services;
        }
    }
}