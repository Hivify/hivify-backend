using SharedKernel;

namespace EventsMgmt.Domain.Events;

public readonly record struct EventID(Guid Value) : IValue
{
}