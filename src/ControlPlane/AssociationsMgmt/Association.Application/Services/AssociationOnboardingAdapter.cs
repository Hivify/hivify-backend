using Association.Application.Commands.CreateAssociation;
using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Services;

public sealed class AssociationOnboardingAdapter(ICommandHandler<CreateAssociationCommand, Guid> commandHandler)
    : IAssociationOnboardingPort
{
    public Task<Guid> CreateAssociationAsync(
        string AssociationName,
        Guid ownerUserId,
        string ownerEmailAddress,
        Guid tierId,
        CancellationToken ct = default)
    {
        var command = new CreateAssociationCommand(AssociationName, ownerUserId, ownerEmailAddress, tierId);

        return commandHandler.Handle(command, ct);
    }
}