using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Microsoft.Extensions.DependencyInjection;
using OnBoarding.Application.Commands.StartOnboarding;
using OnBoarding.Application.DTOs;

namespace OnBoarding.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddOnBoardingServices()
            {

                services.AddScoped<ICommandHandler<StartOnboardingCommand, StartOnboardingResult>, StartOnboardingCommandHandler>();


                return services;
            }


        }
    }
}
