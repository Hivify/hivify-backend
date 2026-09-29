using Association.Application.Contracts;
using Association.Domain.Associations;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValuesObjects;

namespace Association.Application.Commands.AddAssociation;

public sealed class CreateAssociationCommandHandler : ICommandHandler<AddAssociationCommand, AssociationID>
{
    private readonly IAssociationRepo _associationRepository;

    public CreateAssociationCommandHandler(IAssociationRepo associationRepository)
    {
        _associationRepository = associationRepository;
    }

    public async Task<AssociationID> Handle(AddAssociationCommand command, CancellationToken cancellationToken)
    {
        var associationEntity = AssociationEntity.Create(new Name(command.Name));

        await _associationRepository.AddAsync(associationEntity, cancellationToken);

        await _associationRepository.SaveChangesAsync(cancellationToken);

        return associationEntity.Id;
    }
}
