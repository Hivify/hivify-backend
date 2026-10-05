using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.CreateAssociation
{
    public sealed record CreateAssociationCommand(string Name, string Identifier) : ICommand<Guid>;
}
