namespace OnBoarding.Application.Contracts;

public interface IOnboardingService
{
    Task<Guid> StartAsync(string associationName, CancellationToken cancellationToken = default);
}