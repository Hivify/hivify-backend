namespace BuildingBlocks.ApplicationPorts.HivifyUser;

public interface IUserSignInRefresher
{
    Task RefreshAsync();
}