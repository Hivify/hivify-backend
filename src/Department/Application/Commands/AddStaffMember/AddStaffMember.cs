using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Domain.Members;

public sealed record AddStaffMemberCommand(
    Guid DepartmentId,
    Guid UserId,
    string FullName,
    string Email,
    MemberRole Role) : ICommand<MemberID>;

