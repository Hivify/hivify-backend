using Association.Domain.Entities;
using Association.Domain.Enums;
using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using SharedKernel.ValuesObjects;

namespace Association.Application.Commands.CreateAssociation;

public sealed class CreateAssociationCommandHandler : ICommandHandler<CreateAssociationCommand, Guid>
{
    private readonly IAssociationRepository _associationRepository;
    private readonly ICurrentUser _currentUser;

    public CreateAssociationCommandHandler(
        IAssociationRepository associationRepository,
        ICurrentUser currentUser)
    {
        _associationRepository = associationRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateAssociationCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        var userId = _currentUser.UserId;

        var name = new Name(command.Name);



        var association = AssociationEntity.Create(name);

        association.AddMember(new UserID(userId), AssociationRoles.Owner);

        await _associationRepository.AddAsync(association, cancellationToken);

        return association.Id.Value;
    }
}