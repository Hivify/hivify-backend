using SharedKernel;

namespace OnBoarding.Domain.ValueObjects
{
    public readonly record struct OnBoardingID(Guid Value) : IValue
    {
    }
}
