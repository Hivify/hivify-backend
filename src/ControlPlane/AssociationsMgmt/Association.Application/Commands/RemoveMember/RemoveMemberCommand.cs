using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Commands.RemoveMember;

public sealed record RemoveMemberCommand(Guid MemberId) : ICommand<bool>;
