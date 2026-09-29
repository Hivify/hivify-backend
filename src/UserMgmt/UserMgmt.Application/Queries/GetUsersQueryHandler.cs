using BuildingBlocks.ApplicationPorts.Messaging;
using UserMgmt.Application.Contracts;
using UserMgmt.Application.DTOs;

namespace UserMgmt.Application.Queries;

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