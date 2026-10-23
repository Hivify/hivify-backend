using Association.Application.Commands.CreateAssociation;
using BuildingBlocks.ApplicationPorts.AssociationPorts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Services;

public sealed class AssociationOnboardingAdapter(ICommandHandler<CreateAssociationCommand, Guid> commandHandler)
    : IAssociationOnboardingPort
{
    public Task<Guid> CreateAssociationAsync(
        string AssociationName,
        Guid ownerUserId,
        string ownerEmailAddress,
        CancellationToken ct = default)
    {
        var command = new CreateAssociationCommand(AssociationName, ownerUserId, ownerEmailAddress);

        return commandHandler.Handle(command, ct);
    }
}