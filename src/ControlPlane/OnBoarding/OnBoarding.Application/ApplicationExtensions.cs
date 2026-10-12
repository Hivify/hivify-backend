using Microsoft.Extensions.DependencyInjection;

namespace OnBoarding.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddAssociationServices()
            {
                //services.AddScoped<IOnboardingService, OnboardingService>();


                return services;
            }


        }
    }
}
