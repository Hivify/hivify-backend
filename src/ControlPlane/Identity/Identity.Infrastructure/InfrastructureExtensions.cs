using Identity.Application.Contracts;
using Identity.Domain;
using Identity.Infrastructure.DotNETIdentity;
using Identity.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {



            public IServiceCollection AddUserMgmtInfrastructure(string connectionString)
            {

                services.AddDbContextFactory<IdentityDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
                services.AddIdentityCore<DotNETApplicationUser>(options =>
                {
                    options.User.RequireUniqueEmail = true;

                    options.Password.RequiredLength = 8;
                    options.Password.RequireDigit = true;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddRoles<IdentityRole<Guid>>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddSignInManager()
                .AddDefaultTokenProviders();



                services.AddScoped<IUserIdentityService, IdentityService>();
                services.AddSingleton<IEmailSender<DotNETApplicationUser>, IdentityNoOpEmailSender>();



                return services;
            }
        }
    }
}
