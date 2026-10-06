using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.DTOs;

namespace Identity.Application.Queries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserListItem>>;