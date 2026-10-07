using Identity.Application.Contracts;
using Identity.Domain;
using Identity.Infrastructure.DotNETIdentity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure
{
    public static class InfrastructureExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddUserMgmtInfrastructure(string connectionString)
            {



                services.AddScoped<IUserIdentityService, IdentityService>();
                services.AddSingleton<IEmailSender<DotNETApplicationUser>, IdentityNoOpEmailSender>();


                return services;
            }
        }
    }
}
