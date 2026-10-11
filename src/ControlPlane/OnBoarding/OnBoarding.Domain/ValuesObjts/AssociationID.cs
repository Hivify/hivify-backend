using SharedKernel;


namespace OnBoarding.Domain.ValueObjects
{
    public readonly record struct AssociationID(Guid Value) : IValue
    {
    }
}



