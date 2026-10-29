namespace BuildingBlocks.ApplicationPorts.CQRSMessaging;

public interface IQuerySender
{
    Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default);
}