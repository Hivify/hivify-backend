using SharedKernel;

namespace Department.Domain.Members
{
    public readonly record struct MemberID(Guid Value) : IValue
    {
    }
}
