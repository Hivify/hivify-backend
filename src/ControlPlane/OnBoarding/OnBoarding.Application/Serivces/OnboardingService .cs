using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.DTOs;
using OnBoarding.Application.OnBoardingSteps;
using OnBoarding.Domain.Entities;
using OnBoarding.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Serivces
{
    public sealed class OnboardingService(
        IOnboardingRepository onboardingRepository,
        CreateAssociationForUser createAssociationForUser,
        CheckUserAssociation checkUserAssociation,

        ICurrentUser currentUser) : IOnboardingService
    {
        private readonly IOnboardingRepository _onboardingRepository = onboardingRepository;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly CreateAssociationForUser _createAssociationForUser = createAssociationForUser;
        private readonly CheckUserAssociation _checkUserAssociation = checkUserAssociation;



        public async Task<StartOnboardingResult> StartAsync(string associationName, CancellationToken cancellationToken = default)
        {

            var userId = _currentUser.UserId;

            var existingProcess = await _onboardingRepository.GetInProgressByUserIdAsync(
                    userId,
                    cancellationToken);

            if (existingProcess is not null)
            {
                throw new InvalidOperationException(
                    "The user already has an onboarding process in progress.");
            }

            var hasAssociations = await _checkUserAssociation.ExecuteAsync(cancellationToken);

            if (hasAssociations)
            {
                throw new InvalidOperationException(
                    "The user already belongs to an association.");
            }

            var process = OnboardingProcess.Start(new UserID(userId));


            await _onboardingRepository.AddAsync(process, cancellationToken);

            var associationId = await _createAssociationForUser.ExecuteAsync(associationName, cancellationToken);

            process.Complete(new AssociationID(associationId));

            await _onboardingRepository.SaveChangesAsync(cancellationToken);

            return new StartOnboardingResult(
                process.Id.Value,
                process.Status,
                process.CurrentStep);
        }


    }
}