using BuildingBlocks.ApplicationPorts.Messaging;
using Microsoft.Extensions.DependencyInjection;
using UserMgmt.Application.Commands.LoginUser;
using UserMgmt.Application.Commands.RegisterUser;
using UserMgmt.Application.DTOs;
using UserMgmt.Application.Queries;

namespace UserMgmt.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserMgmtServices()
        {


            services.AddScoped<IQueryHandler<GetUsersQuery, IReadOnlyList<UserListItem>>, GetUsersQueryHandler>();
            services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>, RegisterUserCommandHandler>();
            services.AddScoped<ICommandHandler<LoginUserCommand, LoginResultDto>, LoginUserCommandHandler>();

            return services;
        }
    }
}