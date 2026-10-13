using OnBoarding.Application.DTOs;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingService
{

    Task<StartOnboardingResult> StartAsync(string associationName, CancellationToken ct);

}