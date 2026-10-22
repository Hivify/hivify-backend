using Association.Application.DTOs;
using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace Association.Application.Contracts
{
    public interface IAssociationRepository
    {
        Task AddAsync(AssociationEntity association, CancellationToken ct = default);
        Task<AssociationEntity?> GetByIdAsync(AssociationID associationId, CancellationToken ct = default);

        Task<AssociationDto?> GetDetailsByIdAsync(AssociationID associationId, CancellationToken ct = default);

        Task<IReadOnlyList<AssociationEntity>> GetAllAsync(CancellationToken ct = default);

        Task<IReadOnlyList<AssociationEntity>> GetByUserIdAsync(UserID userId, CancellationToken ct = default);

        Task<AssociationEntity?> GetByIdentifierAsync(AssociationIdentifier associationIdentifier, CancellationToken ct = default);

        Task<bool> ExistsForUserAsync(UserID userId, CancellationToken ct = default);

        Task<bool> ExistsWithNameAsync(AssociationName name, CancellationToken ct = default);

        Task SaveChangesAsync(CancellationToken ct);

    }
}

