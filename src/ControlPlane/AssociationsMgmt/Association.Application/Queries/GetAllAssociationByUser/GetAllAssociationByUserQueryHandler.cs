using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetAllAssociationsByUser;

public sealed class GetAllAssociationsByUserQueryHandler(
    IAssociationRepository associationRepository)
    : IQueryHandler<GetAllAssociationsByUserQuery,
        IReadOnlyList<UserAssociationDto>>
{
    public async Task<IReadOnlyList<UserAssociationDto>> Handle(
        GetAllAssociationsByUserQuery query,
        CancellationToken ct)
    {
        if (query.UserId == Guid.Empty)
            throw new ArgumentException("User ID cannot be empty.");

        var userId = new UserID(query.UserId);

        var associations = await associationRepository
            .GetByUserIdAsync(userId, ct);

        return associations
            .Select(a =>
            {
                var membership = a.Members
                    .Single(m => m.UserID == userId);

                return new UserAssociationDto(
                    a.Id.Value,
                    a.Name.Value,
                    a.Status.ToString(),
                    membership.Role.ToString(),
                    membership.JoinedAt
                );
            })
            .ToList();
    }
}