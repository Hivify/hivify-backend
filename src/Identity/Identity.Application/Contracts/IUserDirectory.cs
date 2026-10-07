namespace Identity.Application.Contracts;

public interface IUserDirectory
{
    Task<IReadOnlyList<UserListItem>> GetUsersAsync(
        CancellationToken cancellationToken = default);

    Task<UserListItem?> GetUserByIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default);
}