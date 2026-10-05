using Association.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;

namespace Association.Infrastructure.Persistence;

internal sealed class AssociationRepository(
    AssociationDbContext dbContext) : IAssociationRepository
{
    public async Task<AssociationEntity?> GetByIdAsync(
        AssociationID associationId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .FirstOrDefaultAsync(
                a => a.Id == associationId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(
        AssociationEntity association,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Associations.AddAsync(
            association,
            cancellationToken);
    }
    public async Task<IReadOnlyList<AssociationEntity>> GetByUserIdAsync(
    UserID userId,
    CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .Where(a => a.Members.Any(m => m.UserID == userId))
            .ToListAsync(cancellationToken);
    }
}