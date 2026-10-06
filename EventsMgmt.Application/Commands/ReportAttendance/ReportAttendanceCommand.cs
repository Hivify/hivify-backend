using BuildingBlocks.ApplicationPorts.Messaging;
using EventsMgmt.Domain.Attendances;

namespace EventsMgmt.Application.Commands.ReportAttendance;

public sealed record ReportAttendanceCommand(
    Guid EventId,
    AttendanceStatus Status) : ICommand<bool>;