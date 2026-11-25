using Association.Application.Contracts;
using Wolverine.EntityFrameworkCore;

namespace Association.Infrastructure.Persistence;

public sealed class AssociationOutbox(IDbContextOutbox<AssociationDbContext> outbox)
    : IAssociationOutbox
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