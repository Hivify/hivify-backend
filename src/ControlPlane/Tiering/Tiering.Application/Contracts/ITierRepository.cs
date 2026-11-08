using Tiering.Domain.Entities;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Application.Contracts;

public interface ITierRepository
{
    Task<Tier?> GetByIdAsync(
        TierID tierId,
        CancellationToken ct = default);

    Task<IReadOnlyList<Tier>> GetAllAsync(
        CancellationToken ct = default);

    Task AddAsync(
        Tier tier,
        CancellationToken ct = default);

    Task SaveChangesAsync(
        CancellationToken ct = default);
}