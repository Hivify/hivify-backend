using SharedKernel;

namespace OnBoarding.Domain.ValuesObjts
{
    public readonly record struct OnBoardingID(Guid Value) : IValue
    {
    }
}
