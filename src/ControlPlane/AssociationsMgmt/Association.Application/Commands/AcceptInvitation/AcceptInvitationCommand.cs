using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Commands.AcceptInvitation;

public sealed record AcceptInvitationCommand(string Token) : ICommand<Guid>;