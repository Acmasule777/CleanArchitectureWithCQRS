using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthService.Core.Opetions;

namespace AuthService.Core
{
    public static class CoreAuthDependencyInjection
    {
       public static IServiceCollection authcoreDI(this IServiceCollection services, IConfiguration config)
       {
            services.Configure<ConnectionOpetions>(config.GetSection(ConnectionOpetions.Section));
            return services;
       }
            
    }
}
