namespace BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);
    }
}
