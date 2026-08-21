using Department.Application.Behavior;
using Department.Application.Commands.Department;
using Department.Application.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application
{
    public static class DependencyInjectionApplication
    {
        public static IServiceCollection AddDiDepartmentApp(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjectionApplication).Assembly);
                cfg.AddOpenBehavior(typeof(ValidateDepartmentBehavior<,>));

            });
            
            return services;
        }
    }
}
