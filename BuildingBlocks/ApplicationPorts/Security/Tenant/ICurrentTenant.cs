namespace BuildingBlocks.ApplicationPorts.Security.Tenant;

public interface ICurrentTenant
{
    Guid? TenantId { get; }
    Guid? TierId { get; }
    string? Identifier { get; }
    string? TenantName { get; }
}