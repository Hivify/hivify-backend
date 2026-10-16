using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Application.Queries.GetUserAssociations;
using Association.Application.Services;
using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Association.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationServices()
            {

                services.AddScoped<IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>, GetUserAssociationsQueryHandler>();

                services.AddScoped<IAssociationService, AssociationService>();

                services.AddScoped<ITenantClaimsService, AssociationTenantClaimsService>();
                return services;
            }


        }
    }
}
