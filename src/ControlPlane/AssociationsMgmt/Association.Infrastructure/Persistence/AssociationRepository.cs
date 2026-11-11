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

    /// <summary>
    ///     Gets the association by its id.
    /// </summary>
    /// <param name="associationId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<AssociationEntity?> GetByIdAsync(AssociationID associationId, CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .FirstOrDefaultAsync(
                a => a.Id == associationId,
                ct);
    }



    /// <summary>
    ///      Gets Association Details by its id.
    /// </summary>
    /// <param name="associationId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

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
                    .Where(m => m.Role == AssociationRoles.AssociationAdmin)
                    .Select(m => m.MemberEmail.Value)
                    .FirstOrDefault()!,
                a.Identifier.Value,
                a.Status.ToString(),
                a.Members
                    .Where(m => m.Role == AssociationRoles.AssociationAdmin)
                    .Select(m => m.UserID.Value)
                    .FirstOrDefault(),
                a.Members.Count(),
                a.CreatedDate
            ))
            .FirstOrDefaultAsync(ct);
    }



    /// <summary>
    ///         Get All Associations 
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<AssociationEntity>> GetAllAsync(CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .ToListAsync(ct);
    }


    /// <summary>
    ///   Gets all joined associations by the user id.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<IReadOnlyList<AssociationEntity>> GetByUserIdAsync(UserID userId, CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Include(a => a.Members)
            .Where(a => a.Members.Any(m => m.UserID == userId))
            .ToListAsync(ct);
    }


    /// <summary>
    ///      Adds an association 
    /// </summary>
    /// <param name="association"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task AddAsync(AssociationEntity association, CancellationToken ct = default)
    {
        await dbContext.Associations.AddAsync(
            association,
            ct);
    }


    /// <summary>
    ///      Gets an association by its identifier(is the subdomain)
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<AssociationEntity?> GetByIdentifierAsync(AssociationIdentifier identifier, CancellationToken ct = default)
    {
        return await dbContext.Associations
            .FirstOrDefaultAsync(
                a => a.Identifier == identifier,
                ct);
    }


    /// <summary>
    ///        Checks if an association exists for given UserId
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ct"></param>
    /// <returns></returns>

    public async Task<bool> ExistsForUserAsync(UserID userId, CancellationToken ct = default)
    {
        return await dbContext.Associations.AnyAsync(a => a.Members.Any(m => m.UserID == userId), ct);
    }


    /// <summary>
    ///         Checks if an association exists for the given name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="ct"></param>
    /// <returns></returns>    

    public async Task<bool> ExistsWithNameAsync(AssociationName name, CancellationToken ct = default)
    {
        return await dbContext.Associations
            .AnyAsync(a => a.Name.Value == name.Value, ct);
    }


    /// <summary>
    ///         Checks if an association contains a member with the given email
    /// </summary>
    /// <param name="associationId"></param>
    /// <param name="email"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<bool> IsMemberByEmailAsync(
        AssociationID associationId,
        ContactEmail email,
        CancellationToken ct = default)
    {
        return await dbContext.Associations
            .AnyAsync(
                association =>
                    association.Id == associationId &&
                    association.Members.Any(
                        member => member.MemberEmail.Value == email.Value),
                ct);
    }


    public async Task<int> GetMemberCountAsync(
    AssociationID associationId,
    CancellationToken ct = default)
    {
        return await dbContext.Associations
            .Where(a => a.Id == associationId)
            .SelectMany(a => a.Members)
            .CountAsync(ct);
    }



    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await dbContext.SaveChangesAsync(ct);
    }
}