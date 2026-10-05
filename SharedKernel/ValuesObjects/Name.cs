using SharedKernel.Exceptions;

namespace SharedKernel.ValuesObjects
{
    public sealed record Name : BaseValue<string>
    {
        public Name(string value) : base(Validate(value))
        {
        }

        internal static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new DomainException("Name is required.");

            value = value.Trim();
            if (value.Length > 100)
            {
                throw new DomainException("Name cannot be longer than 100 characters.");
            }
            return value;
        }
    }
}
