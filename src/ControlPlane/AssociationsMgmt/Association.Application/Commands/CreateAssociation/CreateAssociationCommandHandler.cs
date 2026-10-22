using Association.Application.Contracts;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Commands.CreateAssociation;

public sealed class CreateAssociationCommandHandler(IAssociationRepository associationRepository)
 : ICommandHandler<CreateAssociationCommand, Guid>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;

    public async Task<Guid> Handle(CreateAssociationCommand command, CancellationToken ct)
    {
        var ownerId = new UserID(command.OwnerUserId);
        var name = new AssociationName(command.Name);

        if (await _associationRepository.ExistsForUserAsync(ownerId, ct))
        {
            throw new InvalidOperationException(
                "This user already owns an association.");
        }

        if (await _associationRepository.ExistsWithNameAsync(name, ct))
        {
            throw new InvalidOperationException(
                "An association with this name already exists.");
        }

        var association = AssociationEntity.Create(
            new AssociationName(command.Name),
            new UserID(command.OwnerUserId),
            new ContactEmail(command.OwnerEmailAddress)
            );

        await _associationRepository.AddAsync(association, ct);
        await _associationRepository.SaveChangesAsync(ct);

        return association.Id.Value;
    }
}