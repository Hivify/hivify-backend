using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Commands.RemoveStaffMember;

public sealed record RemoveStaffMemberCommand(
    Guid DepartmentId,
    Guid MemberId) : ICommand<bool>;

