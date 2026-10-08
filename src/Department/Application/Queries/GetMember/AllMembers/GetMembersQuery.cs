using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Department.Application.DTOs;

namespace Department.Application.Queries.GetMember.AllMembers;

public sealed record GetMembersQuery(Guid DepartmentId) : IQuery<IReadOnlyList<StaffMemberItem>>;
