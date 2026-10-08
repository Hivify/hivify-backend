using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public readonly record struct UserID : IValue
{
    public Guid Value { get; }

    public UserID(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("Tenant ID cannot be empty.");

        Value = value;
    }
}