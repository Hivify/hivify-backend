using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Association.Application.Queries.GetAllAssociations;

public sealed class GetAllAssociationsQueryHandler(
    IAssociationRepository associationRepository,
    ICurrentUser currentUser)
    : IQueryHandler<GetAllAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly ICurrentUser _currentUser = currentUser;

    public async Task<IReadOnlyList<TenantAssociationOutput>> Handle(
        GetAllAssociationsQuery query,
        CancellationToken cancellationToken)
    {
        if (!_currentUser.IsAuthenticated)
        {
            throw new UnauthorizedAccessException("Authentication is required.");
        }

        if (!_currentUser.IsInRole("Admin"))
        {
            throw new UnauthorizedAccessException(
                "Only application administrators can retrieve all associations.");
        }

        var associations = await _associationRepository.GetAllAsync(cancellationToken);

        return associations
            .Select(a => new TenantAssociationOutput(
                a.Id.Value,
                a.Name.Value,
                a.Identifier.Value,
                ""))
            .ToList();
    }
}