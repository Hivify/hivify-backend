using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Association.Application.Contracts
{
    public interface IAssociationRepository
    {
        Task AddAsync(AssociationEntity association, CancellationToken ct = default);
        Task<AssociationEntity?> GetByIdAsync(
            AssociationID associationId,
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<IReadOnlyList<AssociationEntity>> GetByUserIdAsync(UserID userId, CancellationToken cancellationToken = default);

        Task<AssociationEntity?> GetByIdentifierAsync(
            AssociationIdentifier associationIdentifier,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(CancellationToken cancellationToken);


    }
}

