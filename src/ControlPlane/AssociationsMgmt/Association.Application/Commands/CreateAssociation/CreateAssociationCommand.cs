using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Commands.CreateAssociation;

public sealed record CreateAssociationCommand(
    string Name,
    Guid OwnerUserId,
    string OwnerEmailAddress
) : ICommand<Guid>;