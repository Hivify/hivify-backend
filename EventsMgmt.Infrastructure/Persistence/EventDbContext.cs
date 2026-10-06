using EventsMgmt.Domain.Attendances;
using EventsMgmt.Domain.Events;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;
using System.Reflection.Emit;

namespace EventsMgmt.Infrastructure.Persistence;

public sealed class EventDbContext(
    DbContextOptions<EventDbContext> options)
    : DbContext(options)
{
    public DbSet<Event> Events => Set<Event>();

    public DbSet<EventAttendance> EventAttendances =>
        Set<EventAttendance>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Event>(entity =>
        {
            entity.Property(e => e.Id)
                .HasConversion(
                    id => id.Value,
                    value => new EventID(value));

            entity.Property(e => e.CreatedBy)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value));

            entity.Property(e => e.Title)
                .HasConversion(
                    title => title.Value,
                    value => new Title(value))
                .HasMaxLength(200);

            entity.Property(e => e.Description)
                .HasConversion(
                    description => description.Value,
                    value => new Description(value))
                .HasMaxLength(1000);

            entity.HasMany(e => e.Attendances)
                .WithOne()
                .HasForeignKey(a => a.EventId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<EventAttendance>(entity =>
        {
            entity.Property(a => a.Id)
                .HasConversion(
                    id => id.Value,
                    value => new EventAttendanceID(value));

            entity.Property(a => a.EventId)
                .HasConversion(
                    id => id.Value,
                    value => new EventID(value));

            entity.Property(a => a.UserId)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value));

            entity.Property(a => a.Status)
                .HasConversion<int>();

            entity.HasIndex(a => new
            {
                a.EventId,
                a.UserId
            })
            .IsUnique();
        });
    }
}