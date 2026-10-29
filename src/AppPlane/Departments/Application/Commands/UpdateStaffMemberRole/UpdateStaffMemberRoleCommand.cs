using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Department.Domain.Members;

namespace Department.Application.Commands.UpdateStaffMemberRole;

public sealed record UpdateStaffMemberRoleCommand(
    Guid DepartmentId,
    Guid MemberId,
    MemberRole Role) : ICommand<bool>;

