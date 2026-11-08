using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public sealed record IntNumber : BaseValue<int>
{
    public IntNumber(int value) : base(Validate(value))
    {
    }

    private static int Validate(int value)
    {
        if (value < 0)
            throw new DomainException("Value cannot be negative.");

        return value;
    }
}