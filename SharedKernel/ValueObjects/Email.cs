using SharedKernel.Exceptions;

namespace SharedKernel.ValueObjects;

public sealed record Email : BaseValue<string>
{
    public Email(string value) : base(Validate(value))
    {
    }

    private static string Validate(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new DomainException("Email är obligatoriskt.");
        value = value.Trim();

        if (!IsValidEmail(value))
            throw new DomainException("Felaktig e-postformat.");
        return value;
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}
