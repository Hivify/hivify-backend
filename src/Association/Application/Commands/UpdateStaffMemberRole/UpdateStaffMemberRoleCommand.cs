using Association.Domain.Members;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.UpdateStaffMemberRole;

public sealed record UpdateStaffMemberRoleCommand(
    Guid AssociationId,
    Guid MemberId,
    MemberRole Role) : ICommand<bool>;


