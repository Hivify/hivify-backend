using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.DTOs;
using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Services;

public sealed class OnboardingService(
    ICurrentUser currentUser,
    IOnboardingRepository onboardingRepository,
    IOnboardingOrchestrator orchestrator
    )
    : IOnboardingService
{
    public async Task<StartOnboardingResult> StartAsync(
        string associationName,
        CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        var userId = new UserID(currentUser.UserId);

        var existingProcess =
            await onboardingRepository.GetProcessByUserIdAsync(
                userId,
                ct);

        if (existingProcess is not null)
        {
            return new StartOnboardingResult(
                existingProcess.Id.Value,
                existingProcess.Status);
        }

        var process = UserOnboardingProcess.Start(userId);

        // Step 1: Persist the process first.
        await onboardingRepository.AddProcessAsync(process, ct);

        await onboardingRepository.SaveChangesAsync(ct);

        // Step 2: Execute onboarding steps.
        await orchestrator.ExecuteAsync(associationName, ct);

        // Step 3: Mark onboarding as completed.
        process.Complete();

        await onboardingRepository.SaveChangesAsync(ct);

        return new StartOnboardingResult(process.Id.Value, process.Status);
    }
}