using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payroll.core.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.core
{
    public static class DependencyInjectionPayrollCore
    {
        public static IServiceCollection AddDICorePayroll(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConnectionOpetionsPattern>(config.GetSection(ConnectionOpetionsPattern.Section));
            return services;
        }
    }
}
