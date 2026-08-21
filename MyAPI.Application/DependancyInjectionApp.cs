using Microsoft.Extensions.DependencyInjection;
using MyAPI.Application.Behavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application
{
    public static class DependancyInjectionApp
    {
        public static IServiceCollection AddDiApp(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependancyInjectionApp).Assembly);
                cfg.AddOpenBehavior(typeof(CreageEmployeeValidationBehavior<,>));

            });

            return services;
        }
    }
}
