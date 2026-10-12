using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.DTOs;
using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Commands.StartOnboarding;

public sealed class StartOnboardingCommandHandler(
    ICurrentUser currentUser,
    IOnboardingRepository onboardingRepository) : ICommandHandler<StartOnboardingCommand, StartOnboardingResult>
{
    private readonly ICurrentUser _currentUser = currentUser;
    private readonly IOnboardingRepository _onboardingRepository = onboardingRepository;

    public async Task<StartOnboardingResult> Handle(
        StartOnboardingCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException();
        }

        var existingProcess = await _onboardingRepository.GetProcessInProgressByUserIdAsync(
            _currentUser.UserId,
            cancellationToken);

        if (existingProcess is not null)
        {
            return new StartOnboardingResult(
                existingProcess.Id.Value,
                existingProcess.Status);
        }

        var process = UserOnboardingProcess.Start(new UserID(_currentUser.UserId));

        await _onboardingRepository.AddProcessAsync(process, cancellationToken);
        await _onboardingRepository.SaveChangesAsync(cancellationToken);

        return new StartOnboardingResult(
            process.Id.Value,
            process.Status);
    }
}
