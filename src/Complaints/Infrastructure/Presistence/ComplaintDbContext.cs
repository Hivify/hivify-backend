using Complaints.Domain;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;

namespace Complaints.Infrastructure.Persistence;

public sealed class ComplaintDbContext(
    DbContextOptions<ComplaintDbContext> options)
    : DbContext(options)
{
    public DbSet<Complaint> Complaints => Set<Complaint>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Complaint>(entity =>
        {
            // ID
            entity.Property(c => c.Id)
                .HasConversion(
                    id => id.Value,
                    value => new ComplaintID(value));

            // User ID
            entity.Property(c => c.UserId)
                .HasConversion(
                    userId => userId.Value,
                    value => new UserID(value));
            // Association ID
            entity.Property(c => c.AssociationId)
                .HasConversion(
                    associationId => associationId.Value,
                    value => new AssociationID(value));

            // Title
            entity.OwnsOne(
                c => c.Title,
                title =>
                {
                    title.Property(t => t.Value)
                        .HasColumnName("Title")
                        .HasMaxLength(200)
                        .IsRequired();
                });

            // Description
            entity.OwnsOne(
                c => c.Description,
                description =>
                {
                    description.Property(d => d.Value)
                        .HasColumnName("Description")
                        .HasMaxLength(2000)
                        .IsRequired();
                });

            // Image URL
            entity.Property(c => c.ImageUrl)
                .HasMaxLength(5000)
                .IsRequired(false);

            // Status
            entity.Property(c => c.Status)
                .HasConversion<int>();

            // Created
            entity.Property(c => c.CreatedDate)
                .IsRequired();

            // Updated
            entity.Property(c => c.UpdatedDate)
                .IsRequired(false);

            // Resolved
            entity.Property(c => c.ResolvedDate)
                .IsRequired(false);

            // Admin comment
            entity.Property(c => c.AdminComment)
                .HasMaxLength(1000)
                .IsRequired(false);
        });
    }
}