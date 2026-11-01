namespace BuildingBlocks.ApplicationPorts.CQRSMessaging
{
    public interface ICommandSender
    {
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    }
}
