
using BuildingBlocks.ApplicationPorts.Security.Platform;
using Microsoft.Extensions.Logging;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.DTOs;
using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Services;

public sealed class OnboardingService(
    ICurrentPlatformUser currentUser,
    IAssociationOnboardingPort associationOnboardingPort,
    ITieringOnboardingPort tieringOnboardingPort,
    IOnboardingRepository onboardingRepository,
    ILogger<OnboardingService> logger)
    : IOnboardingService
{
    public async Task<StartOnboardingResult> StartOnboardingAsync(
        string associationName,
        Guid tierId,
        CancellationToken ct = default)
    {
        // 1. Ensure the user is authenticated.
        if (!currentUser.IsAuthenticated)
            throw new UnauthorizedAccessException();

        var userId = currentUser.UserId;

        // 2. Check whether the user already has an onboarding process.
        var existingProcess = await onboardingRepository
            .GetProcessByUserIdAsync(new UserID(userId), ct);

        if (existingProcess is not null)
        {
            logger.LogInformation(
                "User {UserId} already has an onboarding process",
                userId);

            return new StartOnboardingResult(
                existingProcess.Id.Value,
                existingProcess.Status);
        }

        // 3. Validate the selected tier.
        var isTierAvailable = await tieringOnboardingPort
            .IsTierAvailableAsync(tierId, ct);

        if (!isTierAvailable)
        {
            throw new InvalidOperationException(
                "The selected tier does not exist or is not available.");
        }

        // 4. Create and persist the onboarding process.
        var process = UserOnboardingProcess.Start(new UserID(userId));

        logger.LogInformation(
            "Starting onboarding process for user {UserId}",
            userId);

        await onboardingRepository.AddProcessAsync(process, ct);
        await onboardingRepository.SaveChangesAsync(ct);

        logger.LogInformation(
            "Onboarding process persisted for user {UserId}",
            userId);

        // 5. Create the association with the selected tier.
        await associationOnboardingPort.CreateAssociationAsync(
            associationName,
            userId,
            currentUser.EmailAddress,
            tierId,
            ct);

        // 6. Mark onboarding as completed.
        process.Complete();

        await onboardingRepository.SaveChangesAsync(ct);

        logger.LogInformation(
            "Onboarding completed for user {UserId} with status {Status}",
            userId,
            process.Status);

        return new StartOnboardingResult(
            process.Id.Value,
            process.Status);
    }
}
