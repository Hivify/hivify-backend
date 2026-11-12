using Association.Application.Contracts;
using Identity.Application.Contracts;

namespace Hivify.Api.Integrations.Association;

public sealed class AssociationSignInRefresherAdapter(IUserSignInRefresher identitySignInRefresher)
    : IAssociationSignInRefresher
{
    public Task RefreshAsync(CancellationToken ct = default)
        => identitySignInRefresher.RefreshAsync();
}