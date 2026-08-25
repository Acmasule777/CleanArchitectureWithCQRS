using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Notification.Core.Opetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Core
{
    public static class DependencyInjectionCoreLayer
    {
        public static IServiceCollection AddDINoficationCore(this IServiceCollection services, IConfiguration config)
        {

            services.Configure<ConnectionOpetionsPattern>(config.GetSection(ConnectionOpetionsPattern.Section));

            return services;
        }
    }
}
