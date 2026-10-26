
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Commands.InviteMember;

public sealed record InviteMemberCommand(string Email) : ICommand<Guid>;
