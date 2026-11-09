using SharedKernel;

namespace Association.Domain.ValueObjects;

public readonly record struct TierID : IValue
{
    public Guid Value { get; }

    public TierID(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "Tier ID cannot be empty.",
                nameof(value));

        Value = value;
    }
}
