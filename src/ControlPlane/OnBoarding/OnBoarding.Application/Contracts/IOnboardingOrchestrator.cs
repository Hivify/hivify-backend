namespace OnBoarding.Application.Contracts;

public interface IOnboardingOrchestrator
{
    Task<Guid> ExecuteAsync(string associationName, CancellationToken ct = default);
}