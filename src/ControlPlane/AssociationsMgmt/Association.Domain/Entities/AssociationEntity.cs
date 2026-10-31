using Association.Domain.Enums;
using Association.Domain.Events;
using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

namespace Association.Domain.Entities;

public class AssociationEntity : BaseEntity<AssociationID>, IAggregateRoot
{
    public AssociationName Name { get; private set; }
    public AssociationIdentifier Identifier { get; private set; }
    public ContactEmail ContactEmail { get; private set; }
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
        AssociationName name,
        ContactEmail contactEmail)
        : base(id)
    {
        Name = name;
        ContactEmail = contactEmail;
        Identifier = identifier;
        Status = AssociationStatus.Pending;
        CreatedDate = DateTime.UtcNow;
    }




    /// <summary>
    ///     Create an association and add the owner as a member with the Admin role.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ownerId"></param>
    /// <param name="contactEmail"></param>
    /// <returns> The created association. </returns>
    public static AssociationEntity Create(AssociationName name, UserID ownerId, ContactEmail contactEmail)
    {
        var association = new AssociationEntity(
            new AssociationID(Guid.NewGuid()),
            new AssociationIdentifier(Guid.NewGuid()),
            name,
            contactEmail);

        association.AddMember(ownerId, AssociationRoles.AssociationAdmin, contactEmail);

        association.RaiseDomainEvent(new AssociationCreatedDomainEvent(association.Id, ownerId));

        return association;
    }




    /// <summary>
    /// Adds a member to the association.
    /// </summary>
    /// <param name="userId">The ID of the user to add.</param>
    /// <param name="role">The role of the user in the association.</param>
    /// <param name="emailAddress">The email address of the user to add.</param>
    /// <returns></returns>



    public void AddMember(UserID userId, AssociationRoles role, ContactEmail memberEmail)
    {
        if (_members.Any(m => m.UserID == userId))
        {
            throw new InvalidOperationException(
                "User is already a member of this association.");
        }

        _members.Add(Membership.Create(userId, role, memberEmail));
    }
}