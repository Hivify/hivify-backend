using Identity.Application.Contracts;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.DotNETIdentity
{
    public sealed class HivifyUsersDirectory : IUserHivifyDirectory
    {
        private readonly UserManager<DotNETApplicationUser> _userManager;

        public HivifyUsersDirectory(
            UserManager<DotNETApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<UserInfo?> GetByIdAsync(
            Guid userId,
            CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user is null)
                return null;

            return new UserInfo(
                user.Id,
                user.FullName,
                user.Email ?? string.Empty

                );
        }

        public async Task<IReadOnlyList<UserInfo>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _userManager.Users
                .Select(user => new UserInfo(
                    user.Id,
                    user.FullName,
                    user.Email ?? string.Empty))
                .ToListAsync(cancellationToken);
        }
    }
}
