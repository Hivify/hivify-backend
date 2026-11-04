using Association.Application.DTOs;
using BuildingBlocks.ApplicationPorts.CQRSMessaging;

namespace Association.Application.Queries.GetAssociationMembers;

public sealed record GetAssociationMembersQuery
    : IQuery<IReadOnlyList<AssociationMemberDto>>;