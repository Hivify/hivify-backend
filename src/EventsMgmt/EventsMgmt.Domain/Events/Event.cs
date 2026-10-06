using EventsMgmt.Domain.Attendances;
using SharedKernel;
using SharedKernel.Exceptions;
using SharedKernel.ValuesObjects;

namespace EventsMgmt.Domain.Events;

public class Event : BaseEntity<EventID>, IAggregateRoot
{
    private readonly List<EventAttendance> _attendances = [];

    public Title Title { get; private set; }

    public Description Description { get; private set; }

    public UserID CreatedBy { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime EndDate { get; private set; }

    public DateTime CreatedDate { get; private set; }

    public DateTime? DeletedAt { get; private set; }

    public IReadOnlyCollection<EventAttendance> Attendances =>
        _attendances.AsReadOnly();

    private Event()
    {
    }

    private Event(
        EventID id,
        UserID createdBy,
        Title title,
        Description description,
        DateTime startDate,
        DateTime endDate) : base(id)
    {
        CreatedDate = DateTime.UtcNow;
        CreatedBy = createdBy;

        SetTitle(title);
        SetDescription(description);
        SetDateRange(startDate, endDate);
    }

    public static Event CreateEvent(
        UserID createdBy,
        Title title,
        Description description,
        DateTime startDate,
        DateTime endDate)
    {
        return new Event(
            new EventID(Guid.NewGuid()),
            createdBy,
            title,
            description,
            startDate,
            endDate);
    }

    public void Update(
        Title title,
        Description description,
        DateTime startDate,
        DateTime endDate)
    {
        EnsureNotDeleted();

        SetTitle(title);
        SetDescription(description);
        SetDateRange(startDate, endDate);
    }

    public void Delete()
    {
        EnsureNotDeleted();

        DeletedAt = DateTime.UtcNow;
    }

    public void ReportAttendance(
        UserID userId,
        AttendanceStatus status)
    {
        EnsureNotDeleted();

        var existingAttendance =
            _attendances.FirstOrDefault(
                attendance => attendance.UserId == userId);

        if (existingAttendance is null)
        {
            _attendances.Add(
                EventAttendance.Create(
                    Id,
                    userId,
                    status));

            return;
        }

        existingAttendance.UpdateStatus(status);
    }

    private void SetTitle(Title title)
    {
        Title = title;
    }

    private void SetDescription(Description description)
    {
        Description = description;
    }

    private void SetDateRange(
        DateTime startDate,
        DateTime endDate)
    {
        if (endDate <= startDate)
        {
            throw new DomainException(
                "Event end date must be after the start date.");
        }

        StartDate = startDate;
        EndDate = endDate;
    }

    private void EnsureNotDeleted()
    {
        if (DeletedAt != null)
        {
            throw new DomainException(
                "Event has already been deleted.");
        }
    }
}