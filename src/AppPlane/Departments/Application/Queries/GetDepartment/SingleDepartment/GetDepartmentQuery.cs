using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.DTOs;

namespace Department.Application.Queries.GetDepartment.SingleDepartment;

public sealed record GetDepartmentQuery(Guid DepartmentId) : IQuery<DepartmentListItem>;
