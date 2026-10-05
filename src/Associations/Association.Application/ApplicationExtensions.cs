
using Association.Application.Commands.CreateAssociation;
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
                services.AddScoped<ICommandHandler<CreateAssociationCommand, Guid>, CreateAssociationCommandHandler>();

                return services;
            }


        }
    }
}
