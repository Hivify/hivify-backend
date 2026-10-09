using SharedKernel;
using SharedKernel.Exceptions;

namespace Houses.Domain.Houses
{


    public sealed record HouseNumber : BaseValue<string>
    {
        public HouseNumber(string value) : base(Validate(value))
        {
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("House number is required.");

            value = value.Trim();

            if (value.Length > 10)
                throw new DomainException("House number cannot exceed 10 characters.");

            return value;
        }
    }
}
