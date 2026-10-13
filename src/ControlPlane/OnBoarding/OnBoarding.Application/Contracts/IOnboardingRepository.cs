using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingRepository
{
    Task<UserOnboardingProcess?> GetProcessByUserIdAsync(
           UserID userId,
           CancellationToken cancellationToken);

    Task AddProcessAsync(
        UserOnboardingProcess process,
        CancellationToken cancellationToken);

    Task SaveChangesAsync(
        CancellationToken cancellationToken);
}