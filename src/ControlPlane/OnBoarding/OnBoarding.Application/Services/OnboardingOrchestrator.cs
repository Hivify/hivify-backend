using Association.Application.Contracts;
using OnBoarding.Application.Contracts;

namespace OnBoarding.Application.Services;

public sealed class OnboardingOrchestrator(IAssociationService associationService)
    : IOnboardingOrchestrator
{
    public Task<Guid> ExecuteAsync(string associationName, CancellationToken ct = default)
    {
        return associationService.CreateAssociationAsync(associationName, ct);
    }
}