using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using EventsMgmt.Application.Contracts;
using EventsMgmt.Domain.Events;
using SharedKernel.ValuesObjects;

namespace EventsMgmt.Application.Commands.CreateEvent;

public sealed class CreateEventCommandHandler
    : ICommandHandler<CreateEventCommand, Guid>
{
    private readonly IEventRepo _eventRepository;
    private readonly ICurrentUser _currentUser;

    public CreateEventCommandHandler(
        IEventRepo eventRepository,
        ICurrentUser currentUser)
    {
        _eventRepository = eventRepository;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateEventCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (!_currentUser.IsInRole("Admin") ||
            _currentUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "Only administrators can create events.");
        }

        var userId = new UserID(_currentUser.UserId);

        var @event = Event.CreateEvent(
            userId,
            new Title(command.Title),
            new Description(command.Description),
            command.StartDate,
            command.EndDate);

        await _eventRepository.CreateEventAsync(
            @event,
            cancellationToken);

        return @event.Id.Value;
    }
}