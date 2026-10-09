using SharedKernel;
using SharedKernel.Exceptions;

namespace Houses.Domain.Houses
{

    public sealed record PostalCode : BaseValue<string>
    {
        public PostalCode(string value) : base(Validate(value))
        {
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Postal code is required.");

            value = value.Trim();

            if (value.Length > 20)
                throw new DomainException("Postal code cannot exceed 20 characters.");

            return value;
        }
    }
}
