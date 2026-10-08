using BuildingBlocks.ApplicationPorts.Contracts.Messaging;

namespace BuildingBlocks.Infrastructure.Messaging;

public sealed class QuerySender(IServiceProvider serviceProvider) : IQuerySender
{
    public async Task<TResponse> Send<TResponse>(
        IQuery<TResponse> query,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IQueryHandler<,>).MakeGenericType(query.GetType(), typeof(TResponse));
        var handler = serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"No query handler registered for {query.GetType().Name}.");

        var handleMethod = handlerType.GetMethod(nameof(IQueryHandler<IQuery<TResponse>, TResponse>.Handle))
            ?? throw new InvalidOperationException($"No Handle method found for {handlerType.Name}.");

        var task = (Task<TResponse>?)handleMethod.Invoke(handler, new object[] { query, cancellationToken });
        return task is null ? throw new InvalidOperationException($"Handler for {query.GetType().Name} returned no task.") : await task;
    }
}