namespace BuildingBlocks.ApplicationPorts.CurrentTenent
{
    public interface ICurrentTenant
    {
        Guid? AssociationId { get; }

        void Set(Guid associationId);
    }
}
