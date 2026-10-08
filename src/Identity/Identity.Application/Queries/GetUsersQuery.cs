using BuildingBlocks.ApplicationPorts.Contracts.Messaging;
using Identity.Application.DTOs;
namespace Identity.Application.Queries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserInfo>>;