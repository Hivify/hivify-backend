using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public sealed record Description : BaseValue<string>
{
    public Description(string value) : base(Validate(value))
    {
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Content is required.");

        value = value.Trim();

        if (value.Length > 1000)
            throw new DomainException("Content cannot exceed 1000 characters.");

        return value;
    }

}
