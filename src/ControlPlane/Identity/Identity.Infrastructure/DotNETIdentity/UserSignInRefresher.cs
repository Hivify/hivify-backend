using BuildingBlocks.ApplicationPorts.HivifyUser;
using Identity.Domain;
using Microsoft.AspNetCore.Identity;

public sealed class UserSignInRefresher(
    ICurrentHivifyUser currentUser,
    UserManager<DotNETApplicationUser> userManager,
    SignInManager<DotNETApplicationUser> signInManager)
    : IUserSignInRefresher
{
    public async Task RefreshAsync()
    {
        var user = await userManager.FindByIdAsync(
            currentUser.UserId.ToString());

        if (user is null)
            throw new InvalidOperationException("User not found.");

        await signInManager.RefreshSignInAsync(user);
    }
}