
using Association.Application.Contracts;

namespace OnBoarding.Application.OnBoardingSteps;

public sealed class CheckUserAssociation(IAssociationService associationService)
{
    private readonly IAssociationService _associationService = associationService;

    public Task<bool> ExecuteAsync(CancellationToken cancellationToken = default)
    {
        return _associationService.HasAssociationsAsync(cancellationToken);
    }
}
