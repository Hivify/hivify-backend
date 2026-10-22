using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Department.Domain.Departments;

namespace Department.Application.Commands.AddDepartment;

public sealed record AddDepartmentCommand(string Name) : ICommand<DepartmentID>;
