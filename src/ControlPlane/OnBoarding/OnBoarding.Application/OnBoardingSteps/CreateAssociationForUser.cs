using Association.Application.Contracts;

namespace OnBoarding.Application.OnBoardingSteps;

public sealed class CreateAssociationForUser
{
    private readonly IAssociationService _associationService;

    public CreateAssociationForUser(
        IAssociationService associationService)
    {
        _associationService = associationService;
    }

    public async Task<Guid> ExecuteAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _associationService.CreateAssociationAsync(
            name,
            cancellationToken);
    }
}