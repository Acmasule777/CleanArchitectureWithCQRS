using AuthService.Application.Interfaces;
using AuthService.Core.Opetions;
using AuthService.Infrastructure.Persistency;
using AuthService.Infrastructure.Repositories;
using AuthService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;


namespace AuthService.Infrastructure
{
    public static class InfrastructureAuthDependencyInjection
    {
        public static IServiceCollection authInfraDI(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContextAuthService>((serviceprovider, opetions) => opetions.
            UseSqlServer(serviceprovider.GetRequiredService<IOptionsMonitor<ConnectionOpetions>>().CurrentValue.DefaulConnections));

            services.AddScoped<IPasswordHash, PasswordHashService>();
            services.AddScoped<IUserRepository, RegisterUser>();
            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }
}
