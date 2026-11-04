using Association.Application.Contracts;
using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;
using BuildingBlocks.ApplicationPorts.Security.Tenant;
using SharedKernel.ValueObjects;

namespace Association.Application.Queries.GetAssociationMembers;

public sealed class GetAssociationMembersQueryHandler(
    ICurrentTenantUser currentTenantUser,
    IAssociationRepository associationRepository)
    : IQueryHandler<GetAssociationMembersQuery,
        IReadOnlyList<AssociationMemberDto>>
{
    public async Task<IReadOnlyList<AssociationMemberDto>> Handle(
        GetAssociationMembersQuery query,
        CancellationToken ct)
    {
        var associationId = currentTenantUser.AssociationId;

        if (associationId is null)
            return null;

        var association = await associationRepository
            .GetByIdAsync(new AssociationID(associationId.Value), ct)
            ?? throw new KeyNotFoundException("Association not found.");

        return association.Members
            .Select(member => new AssociationMemberDto(
                member.UserID.Value,
                member.Role.ToString(),
                member.JoinedAt
            ))
            .ToList();
    }
}