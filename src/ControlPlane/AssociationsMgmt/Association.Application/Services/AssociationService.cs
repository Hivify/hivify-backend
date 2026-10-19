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

    public async Task<Guid> CreateAssociationAsync(string name, CancellationToken ct = default)
    {
        var userId = _currentUser.UserId;

        var association = AssociationEntity.Create(new AssociationName(name), new UserID(userId));

        await _associationRepository.AddAsync(association, ct);

        await _associationRepository.SaveChangesAsync(ct);

        return association.Id.Value;
    }

    public async Task<TenantAssociationOutput?> GetAssociationByIdentifierAsync(
    Guid identifier,
    CancellationToken ct)
    {
        if (identifier == Guid.Empty)
            return null;

        var association = await _associationRepository.GetByIdentifierAsync(
            new AssociationIdentifier(identifier),
            ct);

        if (association is null)
            return null;

        return new TenantAssociationOutput(
            association.Id.Value,
            association.Name.Value,
            association.Identifier.Value
            );
    }

}