namespace BuildingBlocks.ApplicationPorts.CurrentUserProvider
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);

        string EmailAddress { get; }

    }
}
