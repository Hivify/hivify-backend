namespace BuildingBlocks.ApplicationPorts.AssociationPorts;

public interface IAssociationOnboardingPort
{
    Task<Guid> CreateAssociationAsync(string name, Guid ownerUserId, string ownerEmailAddress, CancellationToken ct = default);
}