using Employee.Infrastructure.Messaging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MyAPI.Application.Interfaces;
using MyAPI.Core.Opetions;
using MyAPI.Infrastructure.messanger;
using MyAPI.Infrastructure.Persistancy;
using MyAPI.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Infrastructure
{
    public static class DependancyInjectionInfra
    {
        public static IServiceCollection AddDiInfra(this IServiceCollection Services)
        {
            Services.AddDbContext<ApplicationDbContext>((serviceprovider, opetions) => opetions.
            UseSqlServer(serviceprovider.GetRequiredService<IOptionsMonitor<ConnectionOpetionsPattern>>().CurrentValue.DefaulConnections));

            Services.AddScoped<IEmployee, EmployeeRepository>();
            Services.AddScoped<IDepartmentServiceClient, DepartmentRepoClient>();
            Services.AddScoped<IPayrollRepositoryClient, PayrollRespositoryClient>();

            Services.AddScoped<IRabbitMQPublisher, RabbitMQPublisher>();
            Services.AddScoped<IPublishDepartmentName, RabbitMQPublisherDepartmentName>();
            Services.AddSingleton<IDepartmentIdResponseService,DepartmentIdResponseService>();

            return Services;
        }

       
    }
}
