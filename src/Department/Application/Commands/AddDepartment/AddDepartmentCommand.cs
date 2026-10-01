using Department.Domain.Departments;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Commands.AddDepartment;

public sealed record AddDepartmentCommand(string Name) : ICommand<DepartmentID>;
