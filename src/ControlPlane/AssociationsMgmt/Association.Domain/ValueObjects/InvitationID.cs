using SharedKernel;

namespace Association.Domain.ValueObjects
{
    public readonly record struct InvitationID(Guid Value) : IValue
    {
    }
}
