namespace BuildingBlocks.ApplicationPorts.CurrentUserPort
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);

        string EmailAddress { get; }

    }
}
