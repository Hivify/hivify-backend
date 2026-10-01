using Department.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;

public sealed record AddStaffMemberCommand(
    Guid DepartmentId,
    Guid UserId,
    string FullName,
    string Email,
    MemberRole Role) : ICommand<MemberID>;

