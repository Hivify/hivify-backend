namespace BuildingBlocks.ApplicationPorts.CurrentTenantPort
{
    public interface ICurrentTenant
    {
        Guid? AssociationId { get; }
    }
}
