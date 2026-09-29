using Feeds.Application.Contracts;
using Feeds.Domain.Feeds;
using Microsoft.EntityFrameworkCore;

namespace Feeds.Infrastructure.Persistence;

public sealed class FeedRepo : IFeedRepo
{
    private readonly FeedDbContext _context;

    public FeedRepo(FeedDbContext context)
    {
        _context = context;
    }

    public async Task CreateFeedAsync(
        Feed feed,
        CancellationToken cancellationToken = default)
    {
        _context.Feeds.Add(feed);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Feed>> GetAllFeedsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Feeds
            .Where(f => f.DeletedAt == null)
            .ToListAsync(cancellationToken);
    }

    public async Task<Feed?> GetByIdAsync(
        FeedID id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Feeds
            .FirstOrDefaultAsync(
                f => f.Id == id,
                cancellationToken);
    }

    public async Task<IEnumerable<Feed>> GetAllByDateAsync(
     DateTime createdDate,
     CancellationToken cancellationToken = default)
    {
        return await _context.Feeds
            .Where(f =>
                f.DeletedAt == null &&
                f.CreatedDate.Date == createdDate.Date)
            .ToListAsync(cancellationToken);
    }
    public async Task UpdateFeedAsync(
        Feed feed,
        CancellationToken cancellationToken = default)
    {
        _context.Feeds.Update(feed);

        await _context.SaveChangesAsync(cancellationToken);
    }
}