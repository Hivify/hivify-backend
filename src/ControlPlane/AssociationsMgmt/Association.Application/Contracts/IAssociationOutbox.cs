namespace Association.Application.Contracts;

public interface IAssociationOutbox
{
    ValueTask PublishAsync<TMessage>(TMessage message)
        where TMessage : notnull;

    Task SaveChangesAndFlushMessagesAsync(
        CancellationToken ct = default);
}