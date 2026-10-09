using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Contracts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetUserAssociations;

public sealed class GetUserAssociationsQueryHandler : IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>
{
    private readonly IAssociationRepository _associationRepository;
    private readonly ICurrentUser _currentUser;

    public GetUserAssociationsQueryHandler(
        IAssociationRepository associationRepository,
        ICurrentUser currentUser)
    {
        _associationRepository = associationRepository;
        _currentUser = currentUser;
    }

    public async Task<IReadOnlyList<TenantAssociationOutput>> Handle(
        GetUserAssociationsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = new UserID(_currentUser.UserId);

        var associations = await _associationRepository
            .GetByUserIdAsync(
                userId,
                cancellationToken);

        return associations
            .Select(a =>
            {
                var membership = a.Members
                    .First(m => m.UserID == userId);

                return new TenantAssociationOutput(
                    a.Id.Value,
                    a.Name.Value,
                    a.Identifier.Value,
                    membership.Role.ToString());
            })
            .ToList();
    }
}