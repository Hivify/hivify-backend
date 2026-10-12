using Association.Application.Contracts;

namespace OnBoarding.Application.OnBoardingSteps;

public sealed class CreateAssociationForUser(IAssociationService associationService)
{
    private readonly IAssociationService _associationService = associationService;

    public async Task<Guid> ExecuteAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _associationService.CreateAssociationAsync(
            name,
            cancellationToken);

    }
}