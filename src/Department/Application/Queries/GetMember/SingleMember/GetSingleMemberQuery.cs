using Department.Application.DTOs;
using BuildingBlocks.ApplicationPorts.Messaging;

namespace Department.Application.Queries.GetMember.SingleMember;

public sealed record GetSingleMemberQuery(Guid DepartmentId, Guid MemberId) : IQuery<StaffMemberItem>;
