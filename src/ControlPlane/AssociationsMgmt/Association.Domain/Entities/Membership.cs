using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using SharedKernel;
using SharedKernel.ValueObjects;

public class Membership : BaseEntity<MembershipID>
{
    public UserID UserID { get; private set; }
    public AssociationEmail MemberEmail { get; private set; }
    public DateTimeOffset JoinedAt { get; private set; } = DateTimeOffset.UtcNow;
    public AssociationRoles Role { get; private set; }

    private Membership() { }

    private Membership(
        MembershipID id,
        UserID userId,
        AssociationEmail memberEmail,
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
        AssociationEmail memberEmail)
    {
        return new Membership(
            new MembershipID(Guid.NewGuid()),
            userId,
            memberEmail,
            role);
    }
}