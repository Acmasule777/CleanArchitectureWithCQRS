using AuthService.Application;
using AuthService.Core;
using AuthService.Infrastructure;

namespace AuthService.API
{
    public static class APIAuthDependencyInjection
    {
        public static IServiceCollection authAPIDI(this IServiceCollection services, IConfiguration config)
        {
            services.authcoreDI(config);
            services.authAppDI(config);
            services.authInfraDI();
            return services;

        }
    }
}
