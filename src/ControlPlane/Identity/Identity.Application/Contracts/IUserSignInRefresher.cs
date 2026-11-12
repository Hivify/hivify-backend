namespace Identity.Application.Contracts;

public interface IUserSignInRefresher
{
    Task RefreshAsync();
}
