using Association.Application.Contracts;
using Hivify.Api.Integrations.Association;
using Hivify.Api.Integrations.Identity;
using Hivify.Api.Integrations.Onboarding;
using Hivify.Api.Integrations.OnBoarding;
using Identity.Application.Contracts;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Integrations;

public static class AddServicesIntegrationsDI
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddServicesIntegrations()
        {

            // Association Service Talking to Tiering for policies

            services.AddScoped<IAssociationTierProvider, AssociationTierAdapter>();
            // OnBoarding Service talking to Tiering service 
            services.AddScoped<ITieringOnboardingPort, TieringOnboardingAdapter>();

            // OnBoarding Service talking to Association service
            services.AddScoped<IAssociationOnboardingPort, AssociationOnboardingAdapter>();


            // Identity Service talking to Association for Claims
            services.AddScoped<IAssociationMemberClaims, AssociationClaimsAdapter>();





            return services;
        }
    }
}

