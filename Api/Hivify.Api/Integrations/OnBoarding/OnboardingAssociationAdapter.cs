using Association.Application.Commands.CreateAssociation;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using OnBoarding.Application.Contracts;

namespace Hivify.Api.Integrations.OnBoarding;

public sealed class OnboardingAssociationAdapter(ICommandHandler<CreateAssociationCommand, Guid> commandHandler)
    : IOnboardingAssociationPort
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