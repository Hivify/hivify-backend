using Association.Application.Commands.CreateAssociation;
using BuildingBlocks.ApplicationPorts.Association;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Services;

public sealed class AssociationOnboardingAdapter(ICommandHandler<CreateAssociationCommand, Guid> commandHandler)
    : IAssociationOnboardingPort
{
    public Task<Guid> CreateAssociationAsync(
        string name,
        Guid ownerUserId,
        CancellationToken ct = default)
    {
        var command = new CreateAssociationCommand(name, ownerUserId);

        return commandHandler.Handle(command, ct);
    }
}