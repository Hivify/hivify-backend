using SharedKernel;
using SharedKernel.Exceptions;

namespace Houses.Domain.Houses
{


    public sealed record Address : BaseValue<string>
    {
        public Address(string value) : base(Validate(value))
        {
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Address is required.");

            value = value.Trim();

            if (value.Length > 200)
                throw new DomainException("Address cannot exceed 200 characters.");

            return value;
        }
    }
}
