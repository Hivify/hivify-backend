using BuildingBlocks.ApplicationPorts.Messaging;
using HivifyUserMgmt.Application.Contracts;
using HivifyUserMgmt.Application.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace HivifyUserMgmt.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddHivifyUserMgmtServices()
        {


            services.AddScoped<IQueryHandler<GetUsersQuery, IReadOnlyList<UserInfo>>, GetUsersQueryHandler>();


            return services;
        }
    }
}