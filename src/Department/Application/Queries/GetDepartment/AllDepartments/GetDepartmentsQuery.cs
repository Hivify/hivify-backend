using Department.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Queries.GetDepartment.AllDepartments;

public sealed record GetDepartmentsQuery : IQuery<IReadOnlyList<DepartmentListItem>>;
