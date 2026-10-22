using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Domain.Entities;
using Association.Domain.Enums;
using Association.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;

namespace Association.Infrastructure.Persistence;

internal sealed class AssociationRepository(AssociationDbContext dbContext) : IAssociationRepository
{
    public async Task<AssociationEntity?> GetByIdAsync(AssociationID associationId, CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .FirstOrDefaultAsync(
                a => a.Id == associationId,
                ct);
    }


    public async Task<AssociationDto?> GetDetailsByIdAsync(
        AssociationID associationId,
        CancellationToken ct = default)
    {
        return await dbContext.Associations
            .AsNoTracking()
            .Where(a => a.Id == associationId)
            .Select(a => new AssociationDto(
                a.Id.Value,
                a.Name.Value,
                a.Members
                    .Where(m => m.Role == AssociationRoles.TenantAdmin)
                    .Select(m => m.MemeberEmail.Value)
                    .FirstOrDefault()!,
                a.Identifier.Value,
                a.Status.ToString(),
                a.Members
                    .Where(m => m.Role == AssociationRoles.TenantAdmin)
                    .Select(m => m.UserID.Value)
                    .FirstOrDefault(),
                a.Members.Count(),
                a.CreatedDate
            ))
            .FirstOrDefaultAsync(ct);
    }


    public async Task<IReadOnlyList<AssociationEntity>> GetAllAsync(
        CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .ToListAsync(ct);
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

    public async Task AddAsync(
        AssociationEntity association,
        CancellationToken cancellationToken = default)
    {
        await dbContext.Associations.AddAsync(
            association,
            cancellationToken);
    }

    public async Task<AssociationEntity?> GetByIdentifierAsync(
        AssociationIdentifier identifier,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Associations
            .FirstOrDefaultAsync(
                a => a.Identifier == identifier,
                cancellationToken);
    }

    public async Task<bool> ExistsForUserAsync(
    UserID userId,
    CancellationToken ct = default)
    {
        return await dbContext.Associations
            .AnyAsync(a => a.Members.Any(m => m.UserID == userId), ct);
    }

    public async Task<bool> ExistsWithNameAsync(
       AssociationName name,
       CancellationToken ct = default)
    {
        return await dbContext.Associations
            .AnyAsync(a => a.Name.Value == name.Value, ct);
    }

    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}