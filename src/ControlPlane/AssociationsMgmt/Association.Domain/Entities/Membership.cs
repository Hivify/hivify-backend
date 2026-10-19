using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Association.Domain.Entities;

public class Membership
{
    public AssociationID AssociationID { get; private set; }
    public UserID UserID { get; private set; }
    public ContactEmail MemeberEmail { get; private set; }
    public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;
    public AssociationRoles Role { get; private set; }

    private Membership()
    {
    }

    private Membership(
        AssociationID associationId,
        UserID userId,
        ContactEmail memberEmail,
        AssociationRoles role)
    {
        AssociationID = associationId;
        UserID = userId;
        MemeberEmail = memberEmail;
        Role = role;
    }

    public static Membership Create(
        AssociationID associationId,
        UserID userId,
        AssociationRoles role,
        ContactEmail memberEmail)
    {
        return new Membership(
            associationId,
            userId,
            memberEmail,
            role);
    }
}