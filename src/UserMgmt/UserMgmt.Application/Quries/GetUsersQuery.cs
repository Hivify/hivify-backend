using BuildingBlocks.ApplicationPorts.Messaging;
using UserMgmt.Application.DTOs;
namespace UserMgmt.Application.Queries;

public sealed record GetUsersQuery : IQuery<IReadOnlyList<UserListItem>>;