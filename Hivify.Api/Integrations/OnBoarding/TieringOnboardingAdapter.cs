using OnBoarding.Application.Contracts;
using Tiering.Application.Contracts;

namespace Hivify.Api.Integrations.Onboarding;

public sealed class TieringOnboardingAdapter(
    ITierService tierService) : ITieringOnboardingPort
{
    public Task<bool> IsTierAvailableAsync(
        Guid tierId,
        CancellationToken ct = default)
    {
        return tierService.IsTierAvailableAsync(tierId, ct);
    }
}