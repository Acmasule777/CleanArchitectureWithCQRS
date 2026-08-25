using Nofication.Application;
using Notification.Core;
using Notification.Infrastructure;

namespace Notification.API
{
    public static class DependencyInjectionAPILayer
    {
        public static IServiceCollection AddDINoficationAPI(this IServiceCollection services, IConfiguration config)
        {
            services.AddDINoficationCore(config);
            services.AddDINoficationApplication();
            services.AddDINoficationInfrastructure(config);

            return services;
        }
    }
}
