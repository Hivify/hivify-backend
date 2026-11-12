namespace Association.Application.Contracts;

public interface IAssociationSignInRefresher
{
    Task RefreshAsync(CancellationToken ct = default);
}