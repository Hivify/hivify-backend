using EventsMgmt.Domain.Events;

namespace EventsMgmt.Application.Contracts;

public interface IEventRepo
{
    Task CreateEventAsync(
        Event @event,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<Event>> GetAllEventsAsync(
        CancellationToken cancellationToken = default);

    Task<Event?> GetByIdAsync(
        EventID id,
        CancellationToken cancellationToken = default);

    Task UpdateEventAsync(
        Event @event,
        CancellationToken cancellationToken = default);
}