namespace Hivify.Api.MultiTenancy;

public sealed class ExtendedTenantInfo : Finbuckle.MultiTenant.Abstractions.TenantInfo
{
    public required string TierId { get; init; }
}