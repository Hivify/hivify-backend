namespace Association.Application.Contracts;

public interface IAssociationOnboardingPort
{
    Task<Guid> CreateAssociationAsync(string name, Guid ownerUserId, string ownerEmailAddress, Guid tierId, CancellationToken ct = default);
}