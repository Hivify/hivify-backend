using BuildingBlocks.ApplicationPorts.Messaging;

namespace EventsMgmt.Application.Commands.CreateEvent;

public sealed record CreateEventCommand(
    string Title,
    string Description,
    DateTime StartDate,
    DateTime EndDate) : ICommand<Guid>;