using SharedKernel;

namespace Association.Domain.ValueObjects;

public readonly record struct AssociationTierID : IValue
{
    public Guid Value { get; }

    public AssociationTierID(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "Tier ID cannot be empty.",
                nameof(value));

        Value = value;
    }
}
