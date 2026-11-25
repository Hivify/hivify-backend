namespace BuildingBlocks.ApplicationPorts.Messaging;

public interface IOutbox
{
    ValueTask PublishAsync<TMessage>(TMessage message) where TMessage : notnull;

    Task SaveChangesAndFlushMessagesAsync(CancellationToken ct = default);
}