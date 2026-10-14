using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Application.Contracts;

public interface IOnboardingRepository
{
    Task AddProcessAsync(UserOnboardingProcess process, CancellationToken ct);

    Task<UserOnboardingProcess?> GetProcessByUserIdAsync(UserID userId, CancellationToken ct);

    Task SaveChangesAsync(CancellationToken ct);
}