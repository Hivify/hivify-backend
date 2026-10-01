using Department.Domain.Members;
using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace Department.Domain.Departments;

public class DepartmentEntity : BaseEntity<DepartmentID>, IAggregateRoot
{
    public Name Name { get; private set; }

    private readonly List<Member> _members = [];

    public IReadOnlyCollection<Member> StaffMembers =>
        _members.AsReadOnly();


    private DepartmentEntity()
    {
    }


    private DepartmentEntity(
        DepartmentID id,
        Name name) : base(id)
    {
        Name = name;
    }


    public static DepartmentEntity Create(Name name)
    {
        return new DepartmentEntity(
            new DepartmentID(Guid.NewGuid()),
            name);
    }


    public Member CreateMember(
        UserID userId,
        Name fullName,
        Email email,
        MemberRole role)
    {
        EnsureMemberDoesNotExist(userId);

        var member = Member.Create(
            Id,
            userId,
            fullName,
            email,
            role);

        _members.Add(member);

        return member;
    }


    public void UpdateMemberRole(
        MemberID memberId,
        MemberRole role)
    {
        var member = _members.FirstOrDefault(member =>
            member.Id == memberId &&
            member.DeletedAt == null);

        if (member is null)
        {
            throw new DomainException(
                "Styrelsemedlemmen finns inte i denna f�rening.");
        }

        member.ChangeRole(role);
    }


    public void RemoveMember(Member member)
    {
        EnsureMemberExists(member);

        member.Delete();
    }


    private void EnsureMemberDoesNotExist(UserID userId)
    {
        if (_members.Any(member =>
            member.UserId == userId &&
            member.DeletedAt == null))
        {
            throw new DomainException(
                "Anv�ndaren �r redan styrelsemedlem i denna f�rening.");
        }
    }


    private void EnsureMemberExists(Member member)
    {
        if (!_members.Contains(member))
        {
            throw new DomainException(
                "Styrelsemedlemmen finns inte i denna f�rening.");
        }
    }
}
