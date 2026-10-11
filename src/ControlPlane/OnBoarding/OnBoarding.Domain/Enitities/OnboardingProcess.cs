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
        public CurrentStep? CurrentStep { get; private set; }

        private OnboardingProcess()
        {
        }

    }
}
