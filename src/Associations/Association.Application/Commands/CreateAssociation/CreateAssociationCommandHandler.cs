using Association.Application.Commands.CreateAssociation;
using Association.Application.Contracts;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

public sealed class CreateAssociationCommandHandler
    : ICommandHandler<CreateAssociationCommand, Guid>
{
    private readonly IAssociationService _associationService;

    public CreateAssociationCommandHandler(
        IAssociationService associationService)
    {
        _associationService = associationService;
    }

    public Task<Guid> Handle(
        CreateAssociationCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        return _associationService.CreateAssociationAsync(
            command.Name,
            cancellationToken);
    }
}