using SharedKernel;

namespace Association.Domain.ValueObjects
{
    public readonly record struct MembershipID(Guid Value) : IValue
    {
    }
}
