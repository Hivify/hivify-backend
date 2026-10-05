using Association.Domain;
using SharedKernel;
using SharedKernel.ValuesObjects;

namespace AppTenant.Domain
{
    public class Association : BaseEntity<AssociationID>, IAggregateRoot
    {
        public Name Name { get; private set; }
        public AssociationIdentifier Identifier { get; private set; }
        public AssociationStatus Status { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public readonly List<Membership> _members = [];

        public IReadOnlyCollection<Membership> Members => _members;
        private Association()
        {
        }
        private Association(
            AssociationID id,
            AssociationIdentifier identifier,
            Name name,
            AssociationStatus status) : base(id)
        {
            CreatedDate = DateTime.UtcNow;
            Name = name;
            Identifier = identifier;
            Status = status;
        }
        public static Association CreateAssociation(
            Name name,
            AssociationStatus status)
        {
            return new Association(
                new AssociationID(Guid.NewGuid()),
                new AssociationIdentifier("default-identifier"),
                name,
                status);
        }

    }
}
