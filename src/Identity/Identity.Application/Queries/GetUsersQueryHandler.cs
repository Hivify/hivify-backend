using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.Contracts;
using Identity.Application.DTOs;

namespace Identity.Application.Queries;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IReadOnlyList<UserListItem>>
{
    private readonly IUserDirectory _userManagementService;

    public GetUsersQueryHandler(
        IUserDirectory userManagementService)
    {
        _userManagementService = userManagementService;
    }

    public async Task<IReadOnlyList<UserListItem>> Handle(
        GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        return await _userManagementService.GetUsersAsync(
            cancellationToken);
    }
}