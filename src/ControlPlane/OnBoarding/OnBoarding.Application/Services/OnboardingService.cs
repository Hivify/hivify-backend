
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
        var userID = new UserID(userId);

        // 2. Check whether the user already has an onboarding process.
        var existingProcess = await onboardingRepository
            .GetProcessByUserIdAsync(userID, ct);

        if (existingProcess is not null)
        {
            logger.LogInformation(
                "User {UserId} already has an onboarding process",
                userId);

            return ToResult(existingProcess);
        }

        // 3. Validate the selected tier through the Onboarding port.
        await EnsureTierIsAvailableAsync(tierId, ct);

        // 4. Create and persist the onboarding process.
        var process = UserOnboardingProcess.Start(userID);

        logger.LogInformation(
            "Starting onboarding process for user {UserId}",
            userId);

        await onboardingRepository.AddProcessAsync(process, ct);
        await onboardingRepository.SaveChangesAsync(ct);

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

        return ToResult(process);
    }

    private async Task EnsureTierIsAvailableAsync(
        Guid tierId,
        CancellationToken ct)
    {
        var isAvailable = await tieringOnboardingPort
            .IsTierAvailableAsync(tierId, ct);

        if (!isAvailable)
        {
            throw new InvalidOperationException(
                "The selected tier does not exist or is not available.");
        }
    }

    private static StartOnboardingResult ToResult(
        UserOnboardingProcess process)
    {
        return new StartOnboardingResult(
            process.Id.Value,
            process.Status);
    }
}
