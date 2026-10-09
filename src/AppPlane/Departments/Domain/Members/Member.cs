using Department.Domain.Departments;
using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValueObjects;
namespace Department.Domain.Members;

public class Member : BaseEntity<MemberID>
{
    public DepartmentID DepartmentId { get; private set; }
    public UserID UserId { get; private set; }

    public Name FullName { get; private set; }
    public Email Email { get; private set; }

    public MemberRole Role { get; private set; }

    public DateTime? DeletedAt { get; private set; }


    private Member() { }


    private Member(MemberID id, DepartmentID departmentId, UserID userId, Name fullName, Email email, MemberRole role) : base(id)
    {
        DepartmentId = departmentId;
        UserId = userId;
        FullName = fullName;
        Email = email;
        Role = role;
    }


    internal static Member Create(DepartmentID departmentId, UserID userId, Name fullName, Email email, MemberRole role)
    {
        return new Member(
            new MemberID(Guid.NewGuid()),
            departmentId,
            userId,
            fullName,
            email,
            role);
    }
    internal void ChangeRole(MemberRole role)
    {
        if (DeletedAt != null)
        {
            throw new DomainException(
                "Styrelsemedlemmen �r borttagen.");
        }

        Role = role;
    }


    internal void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }
}
