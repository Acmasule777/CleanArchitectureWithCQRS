using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nofication.Application
{
    public static class DependencyInjectionApplicationLayer
    {
        public static IServiceCollection AddDINoficationApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionApplicationLayer).Assembly);

            });

            return services;
        }
    }
}
