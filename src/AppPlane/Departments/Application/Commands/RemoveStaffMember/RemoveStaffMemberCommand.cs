using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Department.Application.Commands.RemoveStaffMember;

public sealed record RemoveStaffMemberCommand(
    Guid DepartmentId,
    Guid MemberId) : ICommand<bool>;

