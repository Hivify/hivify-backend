using Association.Application.Contracts;
using Association.Application.DTOs;
using Association.Application.Queries.GetUserAssociations;
using BuildingBlocks.ApplicationPorts.Authorization;
using BuildingBlocks.ApplicationPorts.Messaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetAssociationsForUser;

public sealed class GetAssociationsForUserQueryHandler(
    IAssociationRepository associationRepository,
    IApplicationAuthorization authorization)
    : IQueryHandler<GetAssociationsForUserQuery,
        IReadOnlyList<UserAssociationOutput>>
{
    public async Task<IReadOnlyList<UserAssociationOutput>> Handle(
        GetAssociationsForUserQuery query,
        CancellationToken ct)
    {
        await authorization.EnsureApplicationAdminAsync(ct);

        if (query.UserId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.");

        var userId = new UserID(query.UserId);

        var associations = await associationRepository
            .GetByUserIdAsync(userId, ct);

        return associations
            .Select(a =>
            {
                var membership = a.Members
                    .First(m => m.UserID == userId);

                return new UserAssociationOutput(
                    a.Id.Value,
                    a.Name.Value,
                    a.Status.ToString(),
                    membership.Role.ToString(),
                    membership.JoinedAt);
            })
            .ToList();
    }
}