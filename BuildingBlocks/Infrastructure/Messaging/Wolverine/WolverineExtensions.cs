
using Microsoft.Extensions.Hosting;
using Wolverine;
using Wolverine.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Messaging.Wolverine;

public static class WolverineExtensions
{
    public static IHostBuilder AddWolverineMessaging(
        this IHostBuilder host,
        Action<WolverineOptions> configurePersistence)
    {
        host.UseWolverine(opts =>
        {
            // Shared configuration
            opts.UseEntityFrameworkCoreTransactions();
            opts.Policies.UseDurableLocalQueues();
            opts.UseRuntimeCompilation();

            // Module-specific persistence configuration
            configurePersistence(opts);
        });

        return host;
    }
}
