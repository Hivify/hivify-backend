namespace BuildingBlocks.ApplicationPorts.Security.Platform;

public interface IUserSignInRefresher
{
    Task RefreshAsync();
}
