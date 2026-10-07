using BuildingBlocks.ApplicationPorts.CurrentTenent;

namespace BuildingBlocks.Infrastructure.CurrentTenant
{
    public sealed class CurrentTenant : ICurrentTenant
    {
        public Guid? AssociationId { get; private set; }

        public void Set(Guid associationId)
        {
            AssociationId = associationId;
        }
    }
}
