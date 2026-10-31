namespace SharedKernel.ValueObjects;

public readonly record struct AssociationID : IValue
{
    public Guid Value { get; }

    public AssociationID(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException(
                "Association ID cannot be empty.",
                nameof(value));

        Value = value;
    }
}
