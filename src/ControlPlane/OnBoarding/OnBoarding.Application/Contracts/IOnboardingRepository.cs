using OnBoarding.Domain.Entities;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingRepository
{
    Task AddAsync(OnboardingProcess process, CancellationToken cancellationToken = default);

    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}