using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Payroll.core.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Payroll.Infrastructure.Persistancy;
using Payroll.Application.Interfaces;
using Payroll.Infrastructure.Repositories;

namespace Payroll.Infrastructure
{
    public static class DependencyInjectionPayrollInfrastructure
    {
        public static IServiceCollection AddDIInfraPayroll(this IServiceCollection services)
        {
            services.AddDbContext<AppPayrollDbContext>((serviceprovider, opetions) => opetions.
            UseSqlServer(serviceprovider.GetRequiredService<IOptionsMonitor<ConnectionOpetionsPattern>>().CurrentValue.DefaulConnections));
            services.AddScoped<IPayroll, PayrollRepository>();
            return services;
        }
    }
}
