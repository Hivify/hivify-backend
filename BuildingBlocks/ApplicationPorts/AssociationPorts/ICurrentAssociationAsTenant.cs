namespace BuildingBlocks.ApplicationPorts.AssociationPorts
{
    public interface ICurrentAssociationAsTenant
    {
        Guid? AssociationId { get; }
        Guid? UserId { get; }

        string? AssociationRole { get; }

        bool IsAuthenticated { get; }

        bool IsAssociationTenantAdmin { get; }
    }
}
