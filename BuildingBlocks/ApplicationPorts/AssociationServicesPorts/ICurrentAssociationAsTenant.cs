namespace BuildingBlocks.ApplicationPorts.AssociationServicesPorts
{
    public interface ICurrentAssociationAsTenant
    {
        Guid? AssociationId { get; }
        Guid? UserId { get; }

        string? AssociationMemberRole { get; }

        bool IsAssociationMemberAuthenticated { get; }

        bool IsAssociationMemberAdmin { get; }
    }
}
