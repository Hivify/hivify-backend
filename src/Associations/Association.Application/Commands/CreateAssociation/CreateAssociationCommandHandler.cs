using Association.Domain;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValuesObjects;

namespace Association.Application.Commands.CreateAssociation
{
    public sealed class CreateAssociationCommandHandler : ICommandHandler<CreateAssociationCommand, Guid>
    {

        private readonly IAssociationRepository _associationRepository;
        public CreateAssociationCommandHandler(IAssociationRepository associationRepository)
        {
            _associationRepository = associationRepository;
        }
        public async Task<Guid> Handle(CreateAssociationCommand command, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(command);
            var id = new AssociationID(command.AssociationId);
            var name = new Name(command.Name);
            var identifier = new AssociationIdentifier(command.Identifier);



            var association = AssociationEntity.Create(
                id,
                identifier,
                name

            );
            await _associationRepository.AddAsync(association, cancellationToken);
            return association.Id.Value;
        }
    }
}
