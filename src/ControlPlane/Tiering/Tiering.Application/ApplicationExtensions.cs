using Microsoft.Extensions.DependencyInjection;

namespace Tiering.Application
{
    public static class ApplicationExtensions
    {
        extension(IServiceCollection services)
        {
            public IServiceCollection AddOnBoardingServices()
            {


                return services;
            }


        }
    }
}
