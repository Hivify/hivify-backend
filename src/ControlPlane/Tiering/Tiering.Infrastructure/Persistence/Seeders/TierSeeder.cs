using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.ValueObjects;
using Tiering.Domain.Entities;

namespace Tiering.Infrastructure.Persistence.Seeders;

public static class TierSeeder
{
    public static async Task SeedAsync(
        IServiceProvider serviceProvider,
        CancellationToken ct = default)
    {
        var dbContext = serviceProvider.GetRequiredService<TieringDbContext>();

        if (await dbContext.Tiers.AnyAsync(ct))
            return;

        var tiers = new List<Tier>
        {
            Tier.Create(
                new Name("Free"),
                new IntNumber(5),
                new IntNumber(1)),

            Tier.Create(
                new Name("Basic"),
                new IntNumber(25),
                new IntNumber(3)),

            Tier.Create(
                new Name("Premium"),
                new IntNumber(100),
                new IntNumber(10))
        };

        await dbContext.Tiers.AddRangeAsync(tiers, ct);
        await dbContext.SaveChangesAsync(ct);
    }
}