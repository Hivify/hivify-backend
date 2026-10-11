using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using OnBoarding.Application.Contracts;
using OnBoarding.Application.OnBoardingSteps;
using OnBoarding.Domain.Entities;
using OnBoarding.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Serivces
{
    public sealed class OnboardingService(
        IOnboardingRepository onboardingRepository,
        CreateAssociationForUser createAssociationForUser,
        ICurrentUser currentUser) : IOnboardingService
    {
        private readonly IOnboardingRepository _onboardingRepository = onboardingRepository;
        private readonly ICurrentUser _currentUser = currentUser;
        private readonly CreateAssociationForUser _createAssociationForUser = createAssociationForUser;

        public Task<Guid> StartAsync(Name associationName, CancellationToken cancellationToken = default)
        {
            return StartAsync(associationName.Value, cancellationToken);
        }

        public async Task<Guid> StartAsync(string associationName, CancellationToken cancellationToken = default)
        {
            var userId = _currentUser.UserId;

            var process = OnboardingProcess.Start(new UserID(userId));

            await _onboardingRepository.AddAsync(process, cancellationToken);

            var associationId = await _createAssociationForUser.ExecuteAsync(associationName, cancellationToken);

            process.Complete(new AssociationID(associationId));

            await _onboardingRepository.SaveChangesAsync(cancellationToken);

            return associationId;
        }


    }
}