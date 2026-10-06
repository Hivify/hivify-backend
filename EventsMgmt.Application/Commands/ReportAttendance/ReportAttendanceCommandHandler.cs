using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using EventsMgmt.Application.Contracts;
using EventsMgmt.Domain.Events;
using SharedKernel.ValuesObjects;

namespace EventsMgmt.Application.Commands.ReportAttendance;

public sealed class ReportAttendanceCommandHandler
    : ICommandHandler<ReportAttendanceCommand, bool>
{
    private readonly IEventRepo _eventRepository;
    private readonly ICurrentUser _currentUser;

    public ReportAttendanceCommandHandler(
        IEventRepo eventRepository,
        ICurrentUser currentUser)
    {
        _eventRepository = eventRepository;
        _currentUser = currentUser;
    }

    public async Task<bool> Handle(
        ReportAttendanceCommand command,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(command);

        if (_currentUser.UserId == Guid.Empty)
        {
            throw new UnauthorizedAccessException(
                "You must be logged in to report attendance.");
        }

        var @event = await _eventRepository.GetByIdAsync(
            new EventID(command.EventId),
            cancellationToken);

        if (@event is null)
        {
            throw new KeyNotFoundException(
                $"Event {command.EventId} was not found.");
        }

        @event.ReportAttendance(
            new UserID(_currentUser.UserId),
            command.Status);

        await _eventRepository.UpdateEventAsync(
            @event,
            cancellationToken);

        return true;
    }
}