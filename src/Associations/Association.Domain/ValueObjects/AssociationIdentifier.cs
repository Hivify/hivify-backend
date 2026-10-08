using SharedKernel;


namespace Association.Domain.ValueObjects
{
    public sealed record AssociationIdentifier : BaseValue<Guid>
    {
        public AssociationIdentifier(Guid value) : base(value)
        {
        }

        private static Guid Validate(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("Identifier is required.");
            return value;
        }
    }
}
