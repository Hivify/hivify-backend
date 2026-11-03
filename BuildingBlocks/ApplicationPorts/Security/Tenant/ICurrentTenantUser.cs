namespace BuildingBlocks.ApplicationPorts.Security.Tenant;

public interface ICurrentTenantUser
{
    Guid UserId { get; }

    bool IsAuthenticated { get; }

    string EmailAddress { get; }

    Guid? AssociationId { get; }

    string? AssociationRole { get; }

    bool IsAssociationAdmin { get; }
}
