using Microsoft.EntityFrameworkCore;
using UserMgmt.Application.Contracts;
using UserMgmt.Application.DTOs;

namespace UserMgmt.Infrastructure.Persistence;

public sealed class UserDirectory : IUserDirectory
{
    private readonly IDbContextFactory<UserManagementDbContext> _contextFactory;


    public UserDirectory(
        IDbContextFactory<UserManagementDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<IReadOnlyList<UserListItem>> GetUsersAsync(
        CancellationToken cancellationToken = default)
    {
        await using var context =
            await _contextFactory.CreateDbContextAsync(
                cancellationToken);

        return await context.Users
            .AsNoTracking()
            .Select(user => new UserListItem(
                user.Id,
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.EmailConfirmed))
            .ToListAsync(cancellationToken);
    }

    public async Task<UserListItem?> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        await using var context =
            await _contextFactory.CreateDbContextAsync(
                cancellationToken);

        return await context.Users
            .AsNoTracking()
            .Where(user => user.Id == userId)
            .Select(user => new UserListItem(
                user.Id,
                user.FullName,
                user.UserName,
                user.Email,
                user.PhoneNumber,
                user.EmailConfirmed))
            .FirstOrDefaultAsync(cancellationToken);
    }
}