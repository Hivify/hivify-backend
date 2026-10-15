using OnBoarding.Application.DTOs;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingService
{
    Task<StartOnboardingResult> StartOnboardingAsync(string associationName, CancellationToken ct);
}