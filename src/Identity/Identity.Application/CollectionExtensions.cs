using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.Commands.LoginUser;
using Identity.Application.Commands.RegisterUser;
using Identity.Application.DTOs;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application;


public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddUserMgmtServices()
        {

            services.AddScoped<ICommandHandler<RegisterUserCommand, Guid>, RegisterUserCommandHandler>();
            services.AddScoped<ICommandHandler<LoginUserCommand, LoginUserResult>, LoginUserCommandHandler>();

            return services;
        }
    }
}