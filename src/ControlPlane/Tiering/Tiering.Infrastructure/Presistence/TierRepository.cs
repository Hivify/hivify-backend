using Microsoft.EntityFrameworkCore;
using Tiering.Application.Contracts;
using Tiering.Domain.Entities;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Infrastructure.Persistence;

internal sealed class TierRepository(
    TieringDbContext dbContext) : ITierRepository
{
    public async Task<Tier?> GetByIdAsync(
        TierID tierId,
        CancellationToken ct = default)
    {
        return await dbContext.Tiers
            .FirstOrDefaultAsync(
                t => t.Id == tierId,
                ct);
    }

    public async Task<IReadOnlyList<Tier>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await dbContext.Tiers
            .ToListAsync(ct);
    }

    public async Task AddAsync(
        Tier tier,
        CancellationToken ct = default)
    {
        await dbContext.Tiers.AddAsync(tier, ct);
    }

    public async Task SaveChangesAsync(
        CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}