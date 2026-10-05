using Association.Domain;

public interface IAssociationRepository
{
    Task<AssociationEntity?> GetByIdAsync(
        AssociationID associationId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
        CancellationToken cancellationToken = default);

    Task AddAsync(
        AssociationEntity association,
        CancellationToken cancellationToken = default);
}