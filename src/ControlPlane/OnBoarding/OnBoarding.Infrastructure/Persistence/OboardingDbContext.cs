using Microsoft.EntityFrameworkCore;
using OnBoarding.Domain.Entities;
using OnBoarding.Domain.ValueObjects;
using SharedKernel.ValueObjects;

namespace OnBoarding.Infrastructure.Persistence;

public sealed class OnboardingDbContext(
    DbContextOptions<OnboardingDbContext> options)
    : DbContext(options)
{
    public DbSet<OnboardingProcess> OnboardingProcesses =>
        Set<OnboardingProcess>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<OnboardingProcess>(entity =>
        {
            // Onboarding ID
            entity.Property(o => o.Id)
                .HasConversion(
                    id => id.Value,
                    value => new OnBoardingID(value));

            // User ID
            entity.Property(o => o.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value))
                .IsRequired();

            // Association ID (nullable)
            entity.Property(o => o.AssociationId)
                .HasConversion(
                    id => id!.Value,
                    value => new AssociationID())
                .IsRequired(false);

            // Status
            entity.Property(o => o.Status)
                .HasConversion<int>()
                .IsRequired();

            // Current step
            entity.Property(o => o.CurrentStep)
                .HasConversion<int>()
                .IsRequired();
        });
    }
}