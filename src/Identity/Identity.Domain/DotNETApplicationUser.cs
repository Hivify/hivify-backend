using Microsoft.AspNetCore.Identity;

namespace Identity.Domain
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class DotNETApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }

}
