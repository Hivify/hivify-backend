using SharedKernel;


namespace Association.Domain.ValueObjects
{
    public readonly record struct AssociationID(Guid Value) : IValue
    {
    }
}



