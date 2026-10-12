using OnBoarding.Domain.Entities;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingRepository
{
    Task AddAsync(UserOnboardingProcess process, CancellationToken cancellationToken = default);

    Task<UserOnboardingProcess?> GetInProgressByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}