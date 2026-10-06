using EventsMgmt.Domain.Events;
using SharedKernel;
using SharedKernel.ValuesObjects;

namespace EventsMgmt.Domain.Attendances;

public class EventAttendance : BaseEntity<EventAttendanceID>
{
    public EventID EventId { get; private set; }

    public UserID UserId { get; private set; }

    public AttendanceStatus Status { get; private set; }

    public DateTime ReportedAt { get; private set; }

    private EventAttendance()
    {
    }

    private EventAttendance(
        EventAttendanceID id,
        EventID eventId,
        UserID userId,
        AttendanceStatus status) : base(id)
    {
        EventId = eventId;
        UserId = userId;
        Status = status;
        ReportedAt = DateTime.UtcNow;
    }

    public static EventAttendance Create(
        EventID eventId,
        UserID userId,
        AttendanceStatus status)
    {
        return new EventAttendance(
            new EventAttendanceID(Guid.NewGuid()),
            eventId,
            userId,
            status);
    }

    public void UpdateStatus(AttendanceStatus status)
    {
        Status = status;
        ReportedAt = DateTime.UtcNow;
    }
}