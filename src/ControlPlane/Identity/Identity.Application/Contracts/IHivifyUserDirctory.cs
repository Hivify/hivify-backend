using Identity.Application.DTOs;

namespace Identity.Application.Contracts;

public interface IUserHivifyDirectory
{
    Task<UserInfo?> GetByIdAsync(Guid userId, CancellationToken ct);

    Task<IReadOnlyList<UserInfo>> GetAllAsync(CancellationToken ct);
}