using SharedKernel;


namespace Association.Domain
{
    public sealed record AssociationIdentifier : BaseValue<string>
    {
        public AssociationIdentifier(string value) : base(value)
        {
        }

        private static string Validate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Identifier is required.");
            value = value.Trim();
            if (value.Length > 50)
                throw new ArgumentException("Identifier cannot exceed 50 characters.");
            return value;
        }
    }
}
