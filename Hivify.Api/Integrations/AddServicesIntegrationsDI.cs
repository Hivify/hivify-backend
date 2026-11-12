using Association.Application.Contracts;
using Hivify.Api.Integrations.Association;
using Hivify.Api.Integrations.Identity;
using Hivify.Api.Integrations.OnBoarding;
using Identity.Application.Contracts;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Integrations;

/// <summary>
/// Registers cross-module adapters that connect application contracts
/// owned by individual modules.
/// 
/// Hivify.Api acts as the composition root and integration boundary,
/// keeping the modules independent of one another's implementations.
/// </summary>
public static class AddServicesIntegrationsDI
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Registers all cross-module integration adapters.
        /// </summary>
        public IServiceCollection AddServicesIntegrations()
        {
            services
                .AddAssociationIntegrations()
                .AddOnboardingIntegrations()
                .AddIdentityIntegrations();

            return services;
        }

        /// <summary>
        /// Registers integrations required by the Association module.
        /// </summary>
        private IServiceCollection AddAssociationIntegrations()
        {
            // Association → Tiering:
            // Retrieves tier policies so Association can enforce its business limits.
            services.AddScoped<
                IAssociationTierProvider,
                AssociationTierAdapter>();

            // Association → Identity:
            // Requests a sign-in refresh after Association membership changes.
            services.AddScoped<
                IAssociationSignInRefresher,
                AssociationSignInRefresherAdapter>();

            return services;
        }

        /// <summary>
        /// Registers integrations required by the Onboarding module.
        /// </summary>
        private IServiceCollection AddOnboardingIntegrations()
        {
            // Onboarding → Identity:
            // Refreshes the user's sign-in after onboarding completes.
            services.AddScoped<
                IOnboardingSignInRefresher,
                OnboardingSignInRefresherAdapter>();

            // Onboarding → Tiering:
            // Validates the selected tier during onboarding.
            services.AddScoped<
                IOnboardingTieringPort,
                OnboardingTieringAdapter>();

            // Onboarding → Association:
            // Creates the initial Association for the user.
            services.AddScoped<
                IOnboardingAssociationPort,
                OnboardingAssociationAdapter>();

            return services;
        }


        /// <summary>
        ///        Registers integrations requried by the Identity module
        /// </summary>
        /// <returns></returns>

        private IServiceCollection AddIdentityIntegrations()
        {
            // Identity → Association:
            // Retrieves Association membership claims when creating the user's
            // authentication principal.
            services.AddScoped<
                ITenantMemberClaims,
                IdentityAssociationClaimsAdapter>();

            return services;
        }


    }
}