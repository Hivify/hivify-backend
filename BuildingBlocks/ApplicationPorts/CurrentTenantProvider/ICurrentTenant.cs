namespace BuildingBlocks.ApplicationPorts.CurrentTenantProvider
{
    public interface ICurrentTenant
    {
        Guid AssociationId { get; }
    }
}
