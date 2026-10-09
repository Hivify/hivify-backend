using Houses.Domain.Houses;
using Houses.Domain.Tenants;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;
namespace Houses.Infrastructure.Persistence;

public sealed class HouseDbContext : DbContext
{
    public HouseDbContext(DbContextOptions<HouseDbContext> options) : base(options)
    {
    }

    public DbSet<House> Houses => Set<House>();

    public DbSet<Tenant> Tenants => Set<Tenant>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =====================
        // House
        // =====================

        modelBuilder.Entity<House>(entity =>
        {
            entity.Property(h => h.Id)
                .HasConversion(
                    id => id.Value,
                    value => new HouseID(value));

            // Address
            entity.OwnsOne(
                h => h.Address,
                address =>
                {
                    address.Property(a => a.Value)
                        .HasColumnName("Address")
                        .HasMaxLength(200);
                });

            // House Number
            entity.OwnsOne(
                h => h.HouseNumber,
                houseNumber =>
                {
                    houseNumber.Property(h => h.Value)
                        .HasColumnName("HouseNumber")
                        .HasMaxLength(20);
                });

            // Postal Code
            entity.OwnsOne(
                h => h.PostalCode,
                postalCode =>
                {
                    postalCode.Property(p => p.Value)
                        .HasColumnName("PostalCode")
                        .HasMaxLength(20);
                });
        });

        // =====================
        // Tenant
        // =====================

        modelBuilder.Entity<Tenant>(entity =>
        {
            entity.HasKey(t => t.Id);

            entity.Property(t => t.Id)
                .HasConversion(
                    id => id.Value,
                    value => new TenantID(value))
                .HasColumnName("TenantId");

            entity.Property(t => t.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value))
                .HasColumnName("UserId")
                .IsRequired();

            entity.Property(t => t.Email)
                .HasConversion(
                    email => email.Value,
                    value => new Email(value))
                .HasColumnName("Email")
                .IsRequired();

            entity.Property(t => t.CreatedAt)
                .HasColumnName("CreatedAt")
                .IsRequired();

            entity.Property(t => t.DeletedAt)
                .HasColumnName("DeletedAt");

            // Full Name
            entity.OwnsOne(
                t => t.FullName,
                fullName =>
                {
                    fullName.Property(n => n.Value)
                        .HasColumnName("FullName")
                        .HasMaxLength(200)
                        .IsRequired();
                });

            // Phone Number
            entity.OwnsOne(
                t => t.PhoneNumber,
                phoneNumber =>
                {
                    phoneNumber.Property(p => p.Value)
                        .HasColumnName("PhoneNumber")
                        .HasMaxLength(30)
                        .IsRequired();
                });
        });
    }
}