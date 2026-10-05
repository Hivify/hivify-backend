using Association.Application.DTOs;
using Association.Application.Queries.GetMyAssociations;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValuesObjects;

namespace Association.Application.Queries.GetUserAssociations;

public sealed class GetUserAssociationsQueryHandler : IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<UserAssociationListItem>>
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

    public async Task<IReadOnlyList<UserAssociationListItem>> Handle(
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

                return new UserAssociationListItem(
                    a.Id.Value,
                    a.Name.Value,
                    a.Identifier.Value,
                    membership.Role.ToString());
            })
            .ToList();
    }
}