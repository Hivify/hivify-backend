namespace BuildingBlocks.ApplicationPorts.Security.Tenant;

public interface ICurrentAssociation
{
    Guid? AssociationId { get; }
    Guid? TierId { get; }
    string? Identifier { get; }
    string? AssociationName { get; }
}