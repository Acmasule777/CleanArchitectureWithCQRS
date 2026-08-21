using DepartmentCore.Core.Opetions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentCore.Core
{
    public static class DependancyInjectionCoreDepartment
    {
       public static IServiceCollection AddDiDepartmentCore(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<ConnectionOpetionPattern>(config.GetSection(ConnectionOpetionPattern.Section));
            return services;
            
        }
    }
}
