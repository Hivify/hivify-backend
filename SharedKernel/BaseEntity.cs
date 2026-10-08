namespace SharedKernel;

public abstract class BaseEntity<TId> : IEntity
{


    public TId Id { get; protected set; } = default!;

    protected BaseEntity()
    {
    }

    protected BaseEntity(TId id)
    {
        Id = id;
    }


}
