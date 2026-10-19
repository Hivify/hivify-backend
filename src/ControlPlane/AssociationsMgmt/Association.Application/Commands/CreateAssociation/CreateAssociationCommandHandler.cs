using Association.Application.Contracts;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Commands.CreateAssociation;

public sealed class CreateAssociationCommandHandler(
    IAssociationRepository associationRepository)
    : ICommandHandler<CreateAssociationCommand, Guid>
{
    private readonly IAssociationRepository _associationRepository =
        associationRepository;

    public async Task<Guid> Handle(
        CreateAssociationCommand command,
        CancellationToken ct)
    {
        var association = AssociationEntity.Create(
            new AssociationName(command.Name),
            new UserID(command.OwnerUserId));

        await _associationRepository.AddAsync(association, ct);
        await _associationRepository.SaveChangesAsync(ct);

        return association.Id.Value;
    }
}