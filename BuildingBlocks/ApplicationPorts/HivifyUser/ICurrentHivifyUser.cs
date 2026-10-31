namespace BuildingBlocks.ApplicationPorts.HivifyUser
{
    public interface ICurrentHivifyUser
    {
        Guid UserId { get; }
        bool IsAuthenticated { get; }
        bool IsInRole(string role);

        string EmailAddress { get; }

    }
}
