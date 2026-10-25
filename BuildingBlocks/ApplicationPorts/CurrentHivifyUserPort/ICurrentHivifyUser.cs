namespace BuildingBlocks.ApplicationPorts.CurrentHivifyUserPort
{
    public interface ICurrentHivifyUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);

        string EmailAddress { get; }

    }
}
