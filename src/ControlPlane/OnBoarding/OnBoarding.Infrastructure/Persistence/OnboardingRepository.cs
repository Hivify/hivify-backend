using Microsoft.EntityFrameworkCore;
using OnBoarding.Application.Contracts;
using OnBoarding.Domain.Entities;
using OnBoarding.Domain.Enums;

namespace OnBoarding.Infrastructure.Persistence;

internal sealed class OnboardingRepository(OnboardingDbContext context) : IOnboardingRepository
{
    public async Task AddAsync(
        UserOnboardingProcess process,
        CancellationToken cancellationToken = default)
    {
        await context.OnboardingProcesses.AddAsync(
            process,
            cancellationToken);
    }

    public async Task<UserOnboardingProcess?> GetInProgressByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await context.OnboardingProcesses
            .FirstOrDefaultAsync(
                p => p.UserId.Value == userId && p.Status == OnboardingStatus.InProgress,
                cancellationToken);
    }

    public async Task SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}