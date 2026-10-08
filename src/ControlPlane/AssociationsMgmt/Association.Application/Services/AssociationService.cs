using Association.Application.Contracts;
using Association.Domain.Entities;
using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using SharedKernel.ValuesObjects;

namespace Association.Application.Services;

public sealed class AssociationService : IAssociationService
{
    private readonly IAssociationRepository _associationRepository;
    private readonly ICurrentUser _currentUser;

    public AssociationService(
        IAssociationRepository associationRepository,
        ICurrentUser currentUser)
    {
        _associationRepository = associationRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> CreateAssociationAsync(string name,
        CancellationToken cancellationToken = default)
    {
        var userId = _currentUser.UserId;

        var associationName = new Name(name);

        var association = AssociationEntity.Create(associationName, new UserID(userId));

        await _associationRepository.AddAsync(association, cancellationToken);

        return association.Id.Value;
    }
}