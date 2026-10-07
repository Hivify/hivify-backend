using BuildingBlocks.ApplicationPorts.Messaging;
using Identity.Application.Contracts;

namespace Identity.Application.Queries;

public sealed class GetUsersQueryHandler : IQueryHandler<GetUsersQuery, IReadOnlyList<UserInfo>>
{
    private readonly IUserHivifyDirectory _userManagementService;

    public GetUsersQueryHandler(
        IUserHivifyDirectory userManagementService)
    {
        _userManagementService = userManagementService;
    }

    public async Task<IReadOnlyList<UserInfo>> Handle(
        GetUsersQuery query,
        CancellationToken cancellationToken)
    {
        return await _userManagementService.GetAllAsync(
            cancellationToken);
    }
}