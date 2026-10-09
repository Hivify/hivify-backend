using SharedKernel;


namespace Feeds.Domain.ValueObjects
{
    public readonly record struct AssociationID(Guid Value) : IValue
    {
    }
}



