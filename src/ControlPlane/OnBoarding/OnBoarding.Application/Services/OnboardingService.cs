using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using Microsoft.Extensions.Logging;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.DTOs;
using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Services;

public sealed class OnboardingService(
    ICurrentUser currentUser,
    IAssociationService associationService,
    IOnboardingRepository onboardingRepository,
    ILogger<OnboardingService> logger
    )
    : IOnboardingService
{
    public async Task<StartOnboardingResult> StartOnboardingAsync(string associationName,
        CancellationToken ct = default)
    {
        if (!currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        var userId = new UserID(currentUser.UserId);


        // Check if the user already has an onboarding process. 
        var existingProcess = await onboardingRepository.GetProcessByUserIdAsync(userId, ct);

        if (existingProcess is not null)
        {
            logger.LogInformation("User {userId} already has an onboarding process", userId);
            return new StartOnboardingResult(
                existingProcess.Id.Value,
                existingProcess.Status);
        }

        var process = UserOnboardingProcess.Start(userId);
        logger.LogInformation("Starting onboarding process for user {userId}", userId);

        // Step 1: Persist the process first.
        await onboardingRepository.AddProcessAsync(process, ct);
        logger.LogInformation("Onboarding process persisted for user {userId}", userId);
        await onboardingRepository.SaveChangesAsync(ct);


        //// Step 2: Execute onboarding steps.
        await associationService.CreateAssociationAsync(associationName, ct);



        // Step 3: Mark onboarding as completed.
        process.Complete();

        logger.LogInformation("Onboarding status changed to {Status}", process.Status);

        await onboardingRepository.SaveChangesAsync(ct);

        return new StartOnboardingResult(process.Id.Value, process.Status);
    }
}