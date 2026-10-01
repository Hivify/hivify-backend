using Department.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Commands.UpdateStaffMemberRole;

public sealed record UpdateStaffMemberRoleCommand(
    Guid DepartmentId,
    Guid MemberId,
    MemberRole Role) : ICommand<bool>;

