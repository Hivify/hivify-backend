
using Association.Application.Commands.CreateAssociation;
using Association.Application.DTOs;
using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace Association.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationServices()
            {
                services.AddScoped<ICommandHandler<CreateAssociationCommand, Guid>, CreateAssociationCommandHandler>();
                services.AddScoped<IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>, GetUserAssociationsQueryHandler>();


                return services;
            }


        }
    }
}
