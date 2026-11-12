namespace OnBoarding.Application.Contracts;

public interface IOnboardingAssociationPort
{
    Task<Guid> CreateAssociationAsync(string name, Guid ownerUserId, string ownerEmailAddress, Guid tierId, CancellationToken ct = default);
}