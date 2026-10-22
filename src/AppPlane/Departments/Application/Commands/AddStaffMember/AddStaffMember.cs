using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using Department.Domain.Members;

public sealed record AddStaffMemberCommand(
    Guid DepartmentId,
    Guid UserId,
    string FullName,
    string Email,
    MemberRole Role) : ICommand<MemberID>;

