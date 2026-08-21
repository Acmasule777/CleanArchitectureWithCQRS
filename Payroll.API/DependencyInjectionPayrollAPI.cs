using Payroll.Application;
using Payroll.core;
using Payroll.Infrastructure;

namespace Payroll.API
{
    public static class DependencyInjectionPayrollAPI
    {
        public static IServiceCollection AddDIAPIPayroll(this IServiceCollection services, IConfiguration config)
        {

            services.AddDICorePayroll(config);
            services.AddDIAppPayroll();
            services.AddDIInfraPayroll();
            return services;
        }
    }
}
