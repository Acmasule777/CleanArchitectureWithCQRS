using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyAPI.Core.Opetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Core
{
    public static class DependancyInjectionCore
    {
        public static IServiceCollection AddCoreDI(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConnectionOpetionsPattern>(config.GetSection(ConnectionOpetionsPattern.Section));
            return services;
        }
    }
}
