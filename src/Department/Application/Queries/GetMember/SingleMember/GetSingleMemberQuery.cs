using BuildingBlocks.ApplicationPorts.Messaging;
using Department.Application.DTOs;

namespace Department.Application.Queries.GetMember.SingleMember;

public sealed record GetSingleMemberQuery(Guid DepartmentId, Guid MemberId) : IQuery<StaffMemberItem>;
