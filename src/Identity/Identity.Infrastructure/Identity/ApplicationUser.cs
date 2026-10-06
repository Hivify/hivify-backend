using Microsoft.AspNetCore.Identity;

namespace Identity.Infrastructure.Identity
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string FullName { get; set; } = string.Empty;
    }

}
