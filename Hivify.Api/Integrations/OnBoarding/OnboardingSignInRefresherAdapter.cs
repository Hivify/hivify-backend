using Identity.Application.Contracts;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Integrations.OnBoarding;

public sealed class OnboardingSignInRefresherAdapter(IUserSignInRefresher identitySignInRefresher)
    : IOnboardingSignInRefresher
{
    public Task RefreshAsync(CancellationToken ct = default)
        => identitySignInRefresher.RefreshAsync();
}