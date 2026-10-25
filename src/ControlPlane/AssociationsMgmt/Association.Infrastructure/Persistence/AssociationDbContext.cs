using Association.Domain.Entities;
using Association.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;

namespace Association.Infrastructure.Persistence;

public sealed class AssociationDbContext(DbContextOptions<AssociationDbContext> options) : DbContext(options)
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
            entity.OwnsOne(a => a.Name, name =>
            {
                name.Property(n => n.Value)
                    .HasColumnName("Name")
                    .HasMaxLength(100)
                    .IsRequired();

                name.HasIndex(n => n.Value)
                    .IsUnique();
            });

            entity.OwnsOne(
                 a => a.ContactEmail,
                 name =>
                 {
                     name.Property(n => n.Value)
                         .HasColumnName("ContactEmail")
                         .HasMaxLength(320)
                         .IsRequired();
                 });

            // Association Identifier
            entity.Property(a => a.Identifier)
               .HasConversion(
                   identifier => identifier.Value,
                   value => new AssociationIdentifier(value))
               .HasColumnName("Identifier")
               .HasColumnType("uniqueidentifier")
               .IsRequired();

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

            entity.OwnsMany(a => a.Members, member =>
            {
                member.ToTable("Memberships");

                member.WithOwner()
                    .HasForeignKey("AssociationID");

                member.HasKey(m => m.Id);

                member.Property(m => m.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new MembershipID(value));

                member.Property(m => m.UserID)
                    .HasConversion(
                        id => id.Value,
                        value => new UserID(value));

                member.Property(m => m.Role)
                    .HasConversion<int>();

                member.Property(m => m.JoinedAt)
                    .IsRequired();

                member.OwnsOne(m => m.MemberEmail, email =>
                {
                    email.Property(e => e.Value)
                        .HasColumnName("MemberEmail")
                        .HasMaxLength(320)
                        .IsRequired();
                });

                member.HasIndex("AssociationID", nameof(Membership.UserID))
                    .IsUnique();
            });
        });
    }
}