using OnBoarding.Domain.Entities;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingRepository
{
    Task AddProcessAsync(UserOnboardingProcess process, CancellationToken cancellationToken = default);

    Task<UserOnboardingProcess?> GetProcessInProgressByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}