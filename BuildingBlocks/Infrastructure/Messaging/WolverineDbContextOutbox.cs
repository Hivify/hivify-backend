using BuildingBlocks.ApplicationPorts.Messaging;
using Microsoft.EntityFrameworkCore;
using Wolverine.EntityFrameworkCore;

namespace BuildingBlocks.Infrastructure.Messaging;

public sealed class WolverineDbContextOutbox<TDbContext>(IDbContextOutbox<TDbContext> outbox) : IOutbox
    where TDbContext : DbContext
{
    public ValueTask PublishAsync<TMessage>(TMessage message) where TMessage : notnull
    {
        return outbox.PublishAsync(message);
    }

    public Task SaveChangesAndFlushMessagesAsync(CancellationToken ct = default)
    {
        return outbox.SaveChangesAndFlushMessagesAsync(ct);
    }
}