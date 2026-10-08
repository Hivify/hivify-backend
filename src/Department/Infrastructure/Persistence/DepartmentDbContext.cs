using Department.Domain.Departments;
using Department.Domain.Members;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValueObjects;


namespace Department.Infrastructure.Persistence
{
    public class DepartmentDbContext : DbContext
    {
        public DepartmentDbContext(DbContextOptions<DepartmentDbContext> options)
            : base(options)
        {
        }

        public DbSet<DepartmentEntity> Departments => Set<DepartmentEntity>();

        public DbSet<Member> Members => Set<Member>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =====================
            // DepartmentEntity
            // =====================

            modelBuilder.Entity<DepartmentEntity>(builder =>
            {
                builder.HasKey(a => a.Id);

                builder.Property(a => a.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new DepartmentID(value));

                builder.Property(a => a.Name)
                    .HasConversion(
                        name => name.Value,
                        value => new Name(value));

                builder.HasMany(a => a.StaffMembers)
                    .WithOne()
                    .HasForeignKey(m => m.DepartmentId)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.Navigation(a => a.StaffMembers)
                    .HasField("_members")
                    .UsePropertyAccessMode(PropertyAccessMode.Field);
            });

            // =====================
            // Member
            // =====================

            modelBuilder.Entity<Member>(builder =>
            {
                builder.HasKey(m => m.Id);

                builder.Property(m => m.Id)
                    .HasConversion(
                        id => id.Value,
                        value => new MemberID(value));

                builder.Property(m => m.DepartmentId)
                    .HasConversion(
                        id => id.Value,
                        value => new DepartmentID(value));

                builder.Property(m => m.UserId)
                    .HasConversion(
                        userId => userId.Value,
                        value => new UserID(value))
                    .HasColumnName("UserId")
                    .IsRequired();

                builder.Property(m => m.FullName)
                    .HasConversion(
                        name => name.Value,
                        value => new Name(value))
                    .HasColumnName("FullName")
                    .IsRequired();

                builder.Property(m => m.Email)
                    .HasConversion(
                        email => email.Value,
                        value => new Email(value))
                    .HasColumnName("Email")
                    .IsRequired();

                builder.Property(m => m.Role)
                    .HasConversion<int>()
                    .IsRequired();

                builder.Property(m => m.DeletedAt)
                    .HasColumnName("DeletedAt");

                builder.HasIndex(m => new
                {
                    m.DepartmentId,
                    m.UserId
                })
                .IsUnique()
                .HasFilter("[DeletedAt] IS NULL");
            });
        }
    }
}
