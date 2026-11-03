namespace BuildingBlocks.ApplicationPorts.Security.Platform;

public interface ICurrentPlatformUser
{
    Guid UserId { get; }

    bool IsAuthenticated { get; }

    bool IsInRole(string role);

    string EmailAddress { get; }
}
