using SharedKernel.Exceptions;

namespace SharedKernel.ValuesObjects;


public readonly record struct UserID : IValue
{
    public Guid Value { get; }

    public UserID(Guid value)
    {
        if (value == Guid.Empty)
            throw new DomainException("User ID cannot be empty.");

        Value = value;
    }
}