using OnBoarding.Application.Contracts;
using Tiering.Application.Contracts;

namespace Hivify.Api.Integrations.OnBoarding;

public sealed class OnboardingTieringAdapter(
    ITierService tierService) : IOnboardingTieringPort
{
    public Task<bool> IsTierAvailableAsync(
        Guid tierId,
        CancellationToken ct = default)
    {
        return tierService.IsTierAvailableAsync(tierId, ct);
    }
}