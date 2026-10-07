using BuildingBlocks.ApplicationPorts.Messaging;
using HivifyUserMgmt.Application.Contracts;
namespace HivifyUserMgmt.Application.Queries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserInfo>>;