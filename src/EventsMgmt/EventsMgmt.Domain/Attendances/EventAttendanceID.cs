using SharedKernel;

namespace EventsMgmt.Domain.Attendances;

public readonly record struct EventAttendanceID(Guid Value) : IValue
{
}