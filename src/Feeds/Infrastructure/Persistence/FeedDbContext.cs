using Feeds.Domain.Feeds;
using Microsoft.EntityFrameworkCore;
using SharedKernel.ValuesObjects;

namespace Feeds.Infrastructure.Persistence;

public sealed class FeedDbContext(DbContextOptions<FeedDbContext> options) : DbContext(options)
{
    public DbSet<Feed> Feeds => Set<Feed>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Feed>(entity =>
        {
            entity.Property(f => f.Id)
                .HasConversion(
                    id => id.Value,
                    value => new FeedID(value));

            entity.Property(f => f.AuthorId)
                .HasConversion(
                    id => id.Value,
                    value => new UserID(value));

            entity.Property(f => f.Title)
                .HasConversion(
                    title => title.Value,
                    value => new Title(value))
                .HasMaxLength(200);

            entity.Property(f => f.Content)
                .HasConversion(
                    content => content.Value,
                    value => new Description(value))
                .HasMaxLength(1000);
        });
    }
}