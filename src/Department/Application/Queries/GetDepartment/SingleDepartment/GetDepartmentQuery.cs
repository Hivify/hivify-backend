using Department.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Queries.GetDepartment.SingleDepartment;

public sealed record GetDepartmentQuery(Guid DepartmentId) : IQuery<DepartmentListItem>;
