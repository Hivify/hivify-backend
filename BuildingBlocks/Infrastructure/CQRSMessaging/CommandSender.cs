using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace BuildingBlocks.Infrastructure.CQRSMessaging;

public sealed class CommandSender(IServiceProvider serviceProvider) : ISender
{
    public async Task<TResponse> Send<TResponse>(
        ICommand<TResponse> command,
        CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(ICommandHandler<,>).MakeGenericType(command.GetType(), typeof(TResponse));

        var handler = serviceProvider.GetService(handlerType)
            ?? throw new InvalidOperationException($"No command handler registered for {command.GetType().Name}.");

        var handleMethod = handlerType.GetMethod(nameof(ICommandHandler<ICommand<TResponse>, TResponse>.Handle))
            ?? throw new InvalidOperationException($"No Handle method found for {handlerType.Name}.");

        var task = (Task<TResponse>?)handleMethod.Invoke(handler, new object[] { command, cancellationToken });

        return task is null ? throw new InvalidOperationException($"Handler for {command.GetType().Name} returned no task.") : await task;
    }
}