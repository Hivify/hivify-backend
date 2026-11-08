using Microsoft.EntityFrameworkCore;
using Tiering.Domain.Entities;
using Tiering.Domain.ValueObjcts;

namespace Tiering.Infrastructure.Persistence;

public sealed class TieringDbContext(
    DbContextOptions<TieringDbContext> options) : DbContext(options)
{
    public DbSet<Tier> Tiers => Set<Tier>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Tier>(entity =>
        {
            entity.ToTable("Tiers");

            entity.HasKey(t => t.Id);

            // Tier ID
            entity.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => new TierID(value))
                .IsRequired();

            // Tier name
            entity.OwnsOne(t => t.TierName, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();
            });

            // Maximum members
            entity.OwnsOne(t => t.MaxMembers, maxMembers =>
            {
                maxMembers.Property(m => m.Value)
                    .HasColumnName("MaxMembers")
                    .IsRequired();
            });

            // Maximum administrators
            entity.OwnsOne(t => t.MaxAdministrators, maxAdmins =>
            {
                maxAdmins.Property(a => a.Value)
                    .HasColumnName("MaxAdministrators")
                    .IsRequired();
            });

            // Status
            entity.Property(t => t.Status)
                .HasConversion<int>()
                .IsRequired();
        });
    }
}