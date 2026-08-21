using Microsoft.Extensions.DependencyInjection;
using Payroll.Application.Bahavior;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application
{
    public static class DependencyInjectionPayrollApplication
    {
        public static IServiceCollection AddDIAppPayroll(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionPayrollApplication).Assembly);
                cfg.AddOpenBehavior(typeof(payrollCommandValidatorBehavior<,>));

            });
            return services;
        }
    }
}
