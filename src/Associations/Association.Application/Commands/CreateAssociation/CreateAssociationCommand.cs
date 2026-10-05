using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Commands.CreateAssociation
{
    public sealed record CreateAssociationCommand(
       Guid AssociationId,
       string Name,
       string Identifier

) : ICommand<Guid>;
}
