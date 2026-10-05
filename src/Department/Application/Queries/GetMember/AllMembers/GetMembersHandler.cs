using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.Contracts;
using Department.Application.DTOs;
using Department.Domain.Departments;

namespace Department.Application.Queries.GetMember.AllMembers;

public sealed class GetMembersQueryHandler : IQueryHandler<GetMembersQuery, IReadOnlyList<StaffMemberItem>>
{
    private readonly IDepartmentRepo _departmentRepository;

    public GetMembersQueryHandler(IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IReadOnlyList<StaffMemberItem>> Handle(GetMembersQuery query, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(new DepartmentID(query.DepartmentId), cancellationToken);

        if (department is null)
            return new List<StaffMemberItem>();

        return department.StaffMembers
            .Where(m => m.DeletedAt == null)
            .Select(m => new StaffMemberItem(m.Id.Value, m.FullName.Value, m.Email.Value, m.Role))
            .ToList();
    }
}
