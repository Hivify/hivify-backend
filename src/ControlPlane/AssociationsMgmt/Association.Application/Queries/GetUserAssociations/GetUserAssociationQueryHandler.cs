using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CurrentUserProvider;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetUserAssociations;

public sealed class GetUserAssociationsQueryHandler(
    IAssociationRepository associationRepository,
    ICurrentUser currentUser) : IQueryHandler<GetUserAssociationsQuery, IReadOnlyList<TenantAssociationOutput>>
{
    private readonly IAssociationRepository _associationRepository = associationRepository;
    private readonly ICurrentUser _currentUser = currentUser;

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