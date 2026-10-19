using SharedKernel;
using SharedKernel.Exceptions;

namespace Association.Domain.ValueObjects
{
    public sealed record ContactEmail : BaseValue<string>
    {
        public ContactEmail(string value) : base(Validate(value))
        {
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Email is required.");
            value = value.Trim();

            if (!IsValidEmail(value))
                throw new DomainException("Invalid email format.");
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

}
