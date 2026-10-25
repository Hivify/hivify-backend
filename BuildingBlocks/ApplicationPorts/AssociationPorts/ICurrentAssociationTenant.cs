namespace BuildingBlocks.ApplicationPorts.AssociationPorts
{
    public interface ICurrentAssociationTenant
    {
        Guid? AssociationId { get; }
    }
}
