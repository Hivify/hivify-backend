namespace OnBoarding.Application.Contracts
{
    public interface ITieringOnboardingPort
    {
        Task<bool> IsTierAvailableAsync(
            Guid tierId,
            CancellationToken ct = default);
    }
}
