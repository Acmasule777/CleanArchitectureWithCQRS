using Department.Application.Interfaces;
using Department.Infrastructure.Messaging;
using Department.Infrastructure.Persistency;
using Department.Infrastructure.Repositories;
using DepartmentCore.Core.Opetions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Infrastructure
{
    public static class DependencyInjectionInfra
    {
        public static IServiceCollection AddDiDepartmentIfra(this IServiceCollection services)
        {
            services.AddDbContext<AppDepartmentDbContext>((serviceprovider, opetions) => opetions.
            UseSqlServer(serviceprovider.GetRequiredService<IOptionsMonitor<ConnectionOpetionPattern>>().CurrentValue.DefaulConnections));

            services.AddScoped<IDepartment, DepartmentRepository>();
            services.AddScoped<IDepartmentIdPublisher, DepartmentIdMessagePublisher>();

            return services;
        }
    }
}
