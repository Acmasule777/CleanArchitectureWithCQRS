using MyAPI.Application;
using MyAPI.Core;
using MyAPI.Infrastructure;

namespace MyApI.API
{
    public static class DependancyInjection
    {
        public static IServiceCollection AddDiApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddCoreDI(config);
            services.AddDiApp();
            services.AddDiInfra();
            return services;
        }
    }
}
