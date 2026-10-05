using Association.Domain;
using SharedKernel.ValuesObjects;

public interface IAssociationRepository
{
    Task<AssociationEntity?> GetByIdAsync(
        AssociationID associationId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AssociationEntity>> GetByUserIdAsync(
        UserID userId,
        CancellationToken cancellationToken = default);

    Task AddAsync(
        AssociationEntity association,
        CancellationToken cancellationToken = default);
}