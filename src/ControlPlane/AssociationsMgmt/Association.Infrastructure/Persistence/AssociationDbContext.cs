using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;

namespace Association.Infrastructure.Persistence;

public sealed class AssociationDbContext(
    DbContextOptions<AssociationDbContext> options)
    : DbContext(options)
{
    public DbSet<AssociationEntity> Associations => Set<AssociationEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AssociationEntity>(entity =>
        {
            // Association ID
            entity.Property(a => a.Id)
                .HasConversion(
                    id => id.Value,
                    value => new AssociationID(value));

            // Name
            entity.OwnsOne(
                a => a.Name,
                name =>
                {
                    name.Property(n => n.Value)
                        .HasColumnName("Name")
                        .HasMaxLength(100)
                        .IsRequired();
                });

            // Association Identifier
            entity.OwnsOne(
                a => a.Identifier,
                identifier =>
                {
                    identifier.Property(i => i.Value)
                        .HasColumnName("Identifier")
                        .HasMaxLength(100)
                        .IsRequired();
                });

            // Status
            entity.Property(a => a.Status)
                .HasConversion<int>()
                .IsRequired();

            // Created
            entity.Property(a => a.CreatedDate)
                .IsRequired();

            // Deleted
            entity.Property(a => a.DeletedAt)
                .IsRequired(false);

            // Memberships
            entity.OwnsMany(
                a => a.Members,
                member =>
                {
                    member.ToTable("Memberships");

                    // Composite primary key
                    member.HasKey(m => new
                    {
                        m.AssociationID,
                        m.UserID
                    });

                    // Association ID
                    member.Property(m => m.AssociationID)
                        .HasConversion(
                            id => id.Value,
                            value => new AssociationID(value))
                        .IsRequired();

                    // User ID
                    member.Property(m => m.UserID)
                        .HasConversion(
                            id => id.Value,
                            value => new UserID(value))
                        .IsRequired();

                    // Role
                    member.Property(m => m.Role)
                        .HasConversion<int>()
                        .IsRequired();
                });
        });
    }
}