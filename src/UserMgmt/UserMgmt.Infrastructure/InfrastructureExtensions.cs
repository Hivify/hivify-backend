using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserMgmt.Application.Contracts;
using UserMgmt.Infrastructure.Identity;
using UserMgmt.Infrastructure.Persistence;

namespace UserMgmt.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddUserMgmtInfrastructure(string connectionString)
            {

                services.AddDbContextFactory<UserManagementDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
                services.AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<UserManagementDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

                services.AddScoped<IUserIdentityService, IdentityService>();
                services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
                services.AddScoped<IUserDirectory, UserDirectory>();


                return services;
            }
        }
    }
}
