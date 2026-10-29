namespace BuildingBlocks.ApplicationPorts.CQRSMessaging
{
    public interface ISender
    {
        Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default);
    }
}
