using AuthService.Application.Jwt_Opetion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AuthService.Application
{
    public static class ApplicationAuthDependencyInjection
    {
        public static IServiceCollection authAppDI(this IServiceCollection services, IConfiguration config)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ApplicationAuthDependencyInjection).Assembly);
            });

            services.Configure<JwtOpetion>(config.GetSection(JwtOpetion.Section));
            return services;
        }
    }
}
