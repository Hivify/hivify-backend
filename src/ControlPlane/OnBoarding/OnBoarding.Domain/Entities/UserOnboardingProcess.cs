using OnBoarding.Domain.Enums;
using OnBoarding.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace OnBoarding.Domain.Entities
{
    public class UserOnboardingProcess : BaseEntity<OnBoardingID>, IAggregateRoot
    {
        public UserID UserId { get; private set; }

        public OnboardingStatus Status { get; private set; } = OnboardingStatus.NotStarted;

        public string? CurrentStep { get; private set; }
        public DateTime StartedAt { get; private set; }

        public DateTime? CompletedAt { get; private set; }


        private UserOnboardingProcess()
        {
            // Required by EF Core
        }

        private UserOnboardingProcess(OnBoardingID id, UserID userId) : base(id)
        {
            UserId = userId;
            Status = OnboardingStatus.InProgress;
            StartedAt = DateTime.UtcNow;
            CurrentStep = null;
        }




        public static UserOnboardingProcess Start(UserID userId)
        {
            if (userId.Value == Guid.Empty)
                throw new ArgumentException("User ID is required.", nameof(userId));

            return new UserOnboardingProcess(
                new OnBoardingID(Guid.NewGuid()),
                userId);
        }





        public void Complete()
        {
            if (Status != OnboardingStatus.InProgress)
                throw new InvalidOperationException(
                    "Onboarding is not in progress.");

            Status = OnboardingStatus.Completed;
            CompletedAt = DateTime.UtcNow;
        }
    }
}
