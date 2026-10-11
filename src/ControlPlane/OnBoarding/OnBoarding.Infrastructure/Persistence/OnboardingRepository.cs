using OnBoarding.Application.Contracts;
using OnBoarding.Domain.Entities;

namespace OnBoarding.Infrastructure.Persistence;

internal sealed class OnboardingRepository(OnboardingDbContext context) : IOnboardingRepository
{
    public async Task AddAsync(
        OnboardingProcess process,
        CancellationToken cancellationToken = default)
    {
        await context.OnboardingProcesses.AddAsync(
            process,
            cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}