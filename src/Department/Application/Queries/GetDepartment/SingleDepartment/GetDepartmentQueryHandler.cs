using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.Contracts;
using Department.Application.DTOs;
using Department.Domain.Departments;

namespace Department.Application.Queries.GetDepartment.SingleDepartment;

public sealed class GetDepartmentQueryHandler : IQueryHandler<GetDepartmentQuery, DepartmentListItem>
{
    private readonly IDepartmentRepo _departmentRepository;

    public GetDepartmentQueryHandler(IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<DepartmentListItem?> Handle(GetDepartmentQuery query, CancellationToken cancellationToken)
    {
        var department = await _departmentRepository.GetByIdAsync(new DepartmentID(query.DepartmentId), cancellationToken);

        if (department is null)
            return null;

        return new DepartmentListItem
        {
            Id = department.Id.Value,
            Name = department.Name.Value,
            StaffMembers = department.StaffMembers
                .Where(m => m.DeletedAt == null)
                .Select(m => new StaffMemberItem(m.Id.Value, m.FullName.Value, m.Email.Value, m.Role))
                .ToList()
        };
    }
}
