using BuildingBlocks.ApplicationPorts.Messaging;
using Houses.Application.Commands.AddTenant;
using Houses.Application.Commands.CreateHouse;
using Houses.Application.Commands.DeleteTenant;
using Houses.Application.Commands.UpdateHouse;
using Houses.Application.DTOs;
using Houses.Application.Queries.GetHouse;
using Houses.Application.Queries.GetHouses;
using Houses.Application.Queries.GetHouseTenants;
using Microsoft.Extensions.DependencyInjection;

namespace Houses.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHouseServices()
        {

            services.AddScoped<ICommandHandler<AddHouseCommand, Guid>, AddHouseCommandHandler>();
            services.AddScoped<ICommandHandler<AddHouseTenantCommand, Guid>, AddHouseTenantCommandHandler>();
            services.AddScoped<ICommandHandler<UpdateHouseCommand, bool>, UpdateHouseCommandHandler>();
            services.AddScoped<IQueryHandler<GetHousesQuery, IReadOnlyList<HouseListItem>>, GetHousesQueryHandler>();
            services.AddScoped<IQueryHandler<GetHouseQuery, HouseListItem>, GetHouseQueryHandler>();
            services.AddScoped<ICommandHandler<RemoveHouseTenantCommand, bool>, RemoveHouseTenantCommandHandler>();
            services.AddScoped<IQueryHandler<GetHouseTenantsQuery, IReadOnlyList<TenantListItem>>, GetHouseTenantsQueryHandler>();

            return services;
        }
    }
}