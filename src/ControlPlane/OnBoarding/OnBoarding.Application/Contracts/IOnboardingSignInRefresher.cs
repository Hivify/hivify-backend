namespace OnBoarding.Application.Contracts;

public interface IOnboardingSignInRefresher
{
    Task RefreshAsync(CancellationToken ct = default);
}