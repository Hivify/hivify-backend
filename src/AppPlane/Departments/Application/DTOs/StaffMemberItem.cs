using Department.Domain.Members;

namespace Department.Application.DTOs
{
    public sealed record StaffMemberItem(
        Guid Id,
        string FullName,
        string Email,
        MemberRole Role
    );
}
