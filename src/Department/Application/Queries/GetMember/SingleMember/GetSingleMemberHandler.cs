using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Department.Application.Contracts;
using Department.Application.DTOs;
using Department.Domain.Departments;
using Department.Domain.Members;

namespace Department.Application.Queries.GetMember.SingleMember;

public sealed class GetSingleMemberQueryHandler : IQueryHandler<GetSingleMemberQuery, StaffMemberItem>
{
    private readonly IDepartmentRepo _departmentRepository;

    public GetSingleMemberQueryHandler(IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<StaffMemberItem?> Handle(GetSingleMemberQuery query, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(new DepartmentID(query.DepartmentId), cancellationToken);

        if (department is null)
            return null;

        var member = department.StaffMembers.FirstOrDefault(m => m.Id == new MemberID(query.MemberId) && m.DeletedAt == null);

        if (member is null)
            return null;

        return new StaffMemberItem(member.Id.Value, member.FullName.Value, member.Email.Value, member.Role);
    }
}
