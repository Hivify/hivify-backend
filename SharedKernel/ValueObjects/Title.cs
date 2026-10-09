using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public sealed record Title : BaseValue<string>
{
    public Title(string value) : base(Validate(value))
    {
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Title is required.");

        value = value.Trim();

        if (value.Length > 200)
            throw new DomainException("Title cannot exceed 200 characters.");

        return value;
    }


}
