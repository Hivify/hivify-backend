using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace Association.Application.Commands.CreateAssociation
{
    public sealed record CreateAssociationCommand(string Name) : ICommand<Guid>;
}
