using EventsMgmt.Application.Contracts;
using EventsMgmt.Domain.Events;
using Microsoft.EntityFrameworkCore;

namespace EventsMgmt.Infrastructure.Persistence;

public sealed class EventRepo : IEventRepo
{
    private readonly EventDbContext _context;

    public EventRepo(EventDbContext context)
    {
        _context = context;
    }

    public async Task CreateEventAsync(
        Event @event,
        CancellationToken cancellationToken = default)
    {
        _context.Events.Add(@event);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<IEnumerable<Event>> GetAllEventsAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Where(e => e.DeletedAt == null)
            .OrderBy(e => e.StartDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<Event?> GetByIdAsync(
        EventID id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Events
            .Include(e => e.Attendances)
            .FirstOrDefaultAsync(
                e => e.Id == id,
                cancellationToken);
    }

    public async Task UpdateEventAsync(
        Event @event,
        CancellationToken cancellationToken = default)
    {
        _context.Events.Update(@event);

        await _context.SaveChangesAsync(cancellationToken);
    }
}