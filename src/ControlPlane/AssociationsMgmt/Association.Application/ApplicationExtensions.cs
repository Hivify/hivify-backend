using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Application.Queries.GetUserAssociations;
using Association.Application.Services;
using BuildingBlocks.ApplicationPorts.Messaging;
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


                return services;
            }


        }
    }
}
