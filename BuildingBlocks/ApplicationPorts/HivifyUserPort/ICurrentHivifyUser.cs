namespace BuildingBlocks.ApplicationPorts.HivifyUserPort
{
    public interface ICurrentHivifyUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);

        string EmailAddress { get; }

    }
}
