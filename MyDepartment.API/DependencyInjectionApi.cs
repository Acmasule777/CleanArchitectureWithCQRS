using Department.Application;
using Department.Infrastructure;
using DepartmentCore.Core;

namespace Department.API
{
    public static class DependencyInjectionApi
    {
        public static IServiceCollection AddDiDepartmentApi(this IServiceCollection services, IConfiguration config)
        {
            services.AddDiDepartmentCore(config).AddDiDepartmentApp().AddDiDepartmentIfra();
            //services.AddDiDepartmentApp();
            //services.AddDiDepartmentIfra();
            return services;
        }
    }
}
