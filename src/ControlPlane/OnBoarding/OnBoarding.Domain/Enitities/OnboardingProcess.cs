using OnBoarding.Domain.Enums;
using OnBoarding.Domain.ValueObjects;
using OnBoarding.Domain.ValuesObjts;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace OnBoarding.Domain.Enitities
{
    public class OnboardingProcess : BaseEntity<OnBoardingID>, IAggregateRoot
    {
        public UserID UserId { get; private set; }
        public AssociationID? AssociationId { get; private set; }

        public OnboardingStatus Status { get; private set; }
        public CurrentStep CurrentStep { get; private set; }


        private OnboardingProcess()
        {
            // Required by EF Core
        }

        private OnboardingProcess(OnBoardingID id, UserID userId)
            : base(id)
        {
            UserId = userId;
            Status = OnboardingStatus.InProgress;
            CurrentStep = CurrentStep.AssociationCreation;
        }

        public static OnboardingProcess Start(UserID userId)
        {
            if (userId.Value == Guid.Empty)
                throw new ArgumentException("User ID is required.", nameof(userId));

            return new OnboardingProcess(
                new OnBoardingID(Guid.NewGuid()),
                userId);
        }

        public void CompleteCreateAssociation(AssociationID associationId)
        {
            if (Status != OnboardingStatus.InProgress)
                throw new InvalidOperationException(
                    "Onboarding is not in progress.");

            if (CurrentStep != CurrentStep.AssociationCreation)
                throw new InvalidOperationException(
                    "The current step is not creating an association.");

            if (AssociationId is not null)
                throw new InvalidOperationException(
                    "An association has already been created for this process.");

            if (associationId.Value == Guid.Empty)
                throw new ArgumentException(
                    "Association ID is required.", nameof(associationId));

            AssociationId = associationId;
        }

        public void Complete(AssociationID associationId)
        {
            if (Status != OnboardingStatus.InProgress)
                throw new InvalidOperationException(
                    "Onboarding is not in progress.");

            if (associationId.Value == Guid.Empty)
                throw new ArgumentException(
                    "Association ID is required.", nameof(associationId));

            AssociationId = associationId;
            Status = OnboardingStatus.Completed;
            CurrentStep = CurrentStep.Completion;
        }
    }
}
