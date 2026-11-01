using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Commands.AcceptMemberInvitation;

public sealed record AcceptInvitationCommand(string Token) : ICommand<Guid>;