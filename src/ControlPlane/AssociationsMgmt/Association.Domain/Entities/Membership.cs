using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

public class Membership : BaseEntity<MembershipID>
{
    public UserID UserID { get; private set; }
    public ContactEmail MemberEmail { get; private set; }
    public DateTime JoinedAt { get; private set; } = DateTime.UtcNow;
    public AssociationRoles Role { get; private set; }

    private Membership() { }

    private Membership(
        MembershipID id,
        UserID userId,
        ContactEmail memberEmail,
        AssociationRoles role)
        : base(id)
    {
        UserID = userId;
        MemberEmail = memberEmail;
        Role = role;
    }

    public static Membership Create(
        UserID userId,
        AssociationRoles role,
        ContactEmail memberEmail)
    {
        return new Membership(
            new MembershipID(Guid.NewGuid()),
            userId,
            memberEmail,
            role);
    }
}