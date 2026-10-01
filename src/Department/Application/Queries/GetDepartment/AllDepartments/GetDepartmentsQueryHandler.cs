using Department.Application.Contracts;
using Department.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Queries.GetDepartment.AllDepartments;

public sealed class GetDepartmentsQueryHandler
    : IQueryHandler<
        GetDepartmentsQuery,
        IReadOnlyList<DepartmentListItem>>
{
    private readonly IDepartmentRepo _departmentRepository;

    public GetDepartmentsQueryHandler(
        IDepartmentRepo departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IReadOnlyList<DepartmentListItem>> Handle(
       GetDepartmentsQuery query,
       CancellationToken cancellationToken)
    {
        var departments =
            await _departmentRepository.GetAllAsync(
                cancellationToken);

        return departments
            .Select(department =>
                new DepartmentListItem
                {
                    Id = department.Id.Value,
                    Name = department.Name.Value,

                    StaffMembers = department.StaffMembers
                        .Where(member => member.DeletedAt == null)
                        .Select(member => new StaffMemberItem(
                            member.Id.Value,
                            member.FullName.Value,
                            member.Email.Value,
                            member.Role))
                        .ToList()
                })
            .ToList();
    }
}
