using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.DTOs;

namespace Department.Application.Queries.GetDepartment.AllDepartments;

public sealed record GetDepartmentsQuery : IQuery<IReadOnlyList<DepartmentListItem>>;
