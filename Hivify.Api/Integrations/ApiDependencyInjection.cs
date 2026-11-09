using Hivify.Api.Integrations.Identity;
using Hivify.Api.Integrations.Onboarding;
using Hivify.Api.Integrations.OnBoarding;
using Identity.Application.Contracts;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Integrations;

public static class ApiDependencyInjection
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddServicesIntegrations()
        {

            services.AddScoped<ITieringOnboardingPort, TieringOnboardingAdapter>();

            services.AddScoped<IAssociationOnboardingPort, AssociationOnboardingAdapter>();


            // Identity Service 
            services.AddScoped<IAssociationMemberClaims, AssociationClaimsAdapter>();


            return services;
        }
    }
}

