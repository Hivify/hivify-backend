namespace BuildingBlocks.ApplicationPorts.Security.Tenant;

public interface ICurrentTenant
{
    Guid? AssociationId { get; }
    Guid? TierId { get; }
    string? Identifier { get; }
    string? AssociationName { get; }
}