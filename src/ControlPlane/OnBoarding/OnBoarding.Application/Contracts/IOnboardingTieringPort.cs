namespace OnBoarding.Application.Contracts;

public interface IOnboardingTieringPort
{
    Task<bool> IsTierAvailableAsync(Guid tierId, CancellationToken ct = default);
}