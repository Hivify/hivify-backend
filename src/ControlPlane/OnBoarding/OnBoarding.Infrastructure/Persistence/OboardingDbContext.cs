
using Microsoft.EntityFrameworkCore;
using OnBoarding.Domain.Entities;
using OnBoarding.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace OnBoarding.Infrastructure.Persistence;

public sealed class OnboardingDbContext(DbContextOptions<OnboardingDbContext> options) : DbContext(options)
{
    public DbSet<UserOnboardingProcess> OnboardingProcesses => Set<UserOnboardingProcess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserOnboardingProcess>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.Id)
                .HasConversion(
                    id => id.Value,
                    value => new OnBoardingID(value));

            entity.Property(o => o.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value))
                .IsRequired();

            entity.Property(o => o.Status)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(o => o.StartedAt)
                .IsRequired();

            entity.Property(o => o.CompletedAt)
                .IsRequired(false);

            entity.HasIndex(o => o.UserId).IsUnique();
        });
    }
}
