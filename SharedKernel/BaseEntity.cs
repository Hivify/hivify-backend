namespace SharedKernel
{
    public abstract class BaseEntity<TId> : IEntity
    {
        private readonly List<IDomainEvent> _domainEvents = [];

        public TId Id { get; protected set; } = default!;


        public IReadOnlyCollection<IDomainEvent> DomainEvents =>
    _domainEvents.AsReadOnly();

        protected BaseEntity()
        {
        }

        protected BaseEntity(TId id)
        {
            Id = id;
        }


        protected void RaiseDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }


    }
}
