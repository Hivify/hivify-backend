using Microsoft.Extensions.DependencyInjection;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.Serivces;

namespace OnBoarding.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationServices()
            {
                services.AddScoped<IOnboardingService, OnboardingService>();


                return services;
            }


        }
    }
}
