using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public sealed record PhoneNumber : BaseValue<string>
{
    public PhoneNumber(string value) : base(Validate(value))
    {
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Telefonnummer är obligatoriskt.");

        value = value.Trim();

        if (!System.Text.RegularExpressions.Regex.IsMatch(value, @"^\+?[0-9\s\-()]{7,20}$"))
            throw new DomainException("Telefonnummer är ogiltigt. Använd format: +46 70 000 00 00");

        return value;
    }
}