using Microsoft.EntityFrameworkCore;
using OnBoarding.Application.Contracts;
using OnBoarding.Domain.Entities;
using SharedKernel.ValueObjects;

namespace OnBoarding.Infrastructure.Persistence;

internal sealed class OnboardingRepository(OnboardingDbContext context) : IOnboardingRepository
{



    public async Task AddProcessAsync(UserOnboardingProcess process, CancellationToken cancellationToken = default)
    {
        await context.OnboardingProcesses.AddAsync(
            process,
            cancellationToken);
    }




    public async Task<UserOnboardingProcess?> GetProcessByUserIdAsync(UserID userId, CancellationToken ct = default)
    {
        return await context.OnboardingProcesses
            .FirstOrDefaultAsync(p => p.UserId.Value == userId.Value, ct);
    }





    public async Task SaveChangesAsync(CancellationToken ct = default)
    {
        await context.SaveChangesAsync(ct);
    }




}