using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.Contracts;
namespace Identity.Application.Queries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserInfo>>;