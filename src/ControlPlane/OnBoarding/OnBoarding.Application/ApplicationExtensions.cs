using Microsoft.Extensions.DependencyInjection;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.Services;

namespace OnBoarding.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddOnBoardingServices()
            {

                services.AddScoped<IOnboardingService, OnboardingService>();
                services.AddScoped<IOnboardingOrchestrator, OnboardingOrchestrator>();

                return services;
            }


        }
    }
}
