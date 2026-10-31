namespace BuildingBlocks.ApplicationPorts.AssociationUserPort
{
    public interface ICurrentAssociationMember
    {
        Guid? AssociationId { get; }
        Guid? UserId { get; }

        string? AssociationMemberRole { get; }

        bool IsAuthenticated { get; }

        bool IsAssociationAdmin { get; }
    }
}
