using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace Association.Domain.Entities;

public class AssociationEntity : BaseEntity<AssociationID>, IAggregateRoot
{
    public Name Name { get; private set; }
    public AssociationIdentifier Identifier { get; private set; }
    public AssociationStatus Status { get; private set; }
    public DateTime CreatedDate { get; private set; }
    public DateTime? DeletedAt { get; private set; }

    private readonly List<Membership> _members = new();

    public IReadOnlyCollection<Membership> Members => _members;

    private AssociationEntity()
    {
    }

    private AssociationEntity(
        AssociationID id,
        AssociationIdentifier identifier,
        Name name)
        : base(id)
    {
        Name = name;
        Identifier = identifier;
        Status = AssociationStatus.Active;
        CreatedDate = DateTime.UtcNow;
    }

    public static AssociationEntity Create(
        AssociationIdentifier identifier,
        Name name)
    {
        return new AssociationEntity(
            new AssociationID(Guid.NewGuid()),
            identifier,
            name);
    }

    public void AddMember(UserID userId, AssociationRoles role)
    {
        if (_members.Any(m => m.UserID == userId))
        {
            throw new InvalidOperationException(
                "User is already a member of this association.");
        }

        _members.Add(Membership.Create(Id, userId, role));
    }
}