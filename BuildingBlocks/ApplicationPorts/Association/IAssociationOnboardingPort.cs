namespace BuildingBlocks.ApplicationPorts.Association;

public interface IAssociationOnboardingPort
{
    Task<Guid> CreateAssociationAsync(string name, Guid ownerUserId, CancellationToken ct = default);
}