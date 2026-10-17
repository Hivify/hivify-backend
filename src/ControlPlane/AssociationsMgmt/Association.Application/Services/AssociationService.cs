using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using SharedKernel.ValueObjects;

namespace Association.Application.Services;

public sealed class AssociationService(
    IAssociationRepository associationRepository,
    ICurrentUser currentUser) : IAssociationService
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<Guid> CreateAssociationAsync(string name,
        CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;

        var associationName = new AssociationName(name);

        var association = AssociationEntity.Create(associationName, new UserID(userId));

        await _associationRepository.AddAsync(association, cancellationToken);

        await _associationRepository.SaveChangesAsync(cancellationToken);

        return association.Id.Value;
    }

    public async Task<TenantAssociationOutput?> GetByIdentifierAsync(
    Guid identifier,
    CancellationToken cancellationToken = default)
    {
        if (identifier == Guid.Empty)
            return null;

        var association = await _associationRepository.GetByIdentifierAsync(
            new AssociationIdentifier(identifier),
            cancellationToken);

        if (association is null)
            return null;

        return new TenantAssociationOutput(
            association.Id.Value,
            association.Name.Value,
            association.Identifier.Value
            );
    }

}