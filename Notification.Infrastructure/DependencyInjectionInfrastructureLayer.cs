using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Nofication.Application.Interfaces;
using Notification.Core.Opetions;
using Notification.Infrastructure.ConfigurationsSettings;
using Notification.Infrastructure.Persistency;
using Notification.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure
{
    public static class DependencyInjectionInfrastructureLayer
    {
        public static IServiceCollection AddDINoficationInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<ApplicationNotificationDbContex>((serviceprovider, opetions) => opetions
            .UseSqlServer(serviceprovider.GetRequiredService<IOptionsMonitor<ConnectionOpetionsPattern>>().CurrentValue.DefaulConnections));

            services.Configure<EmailSettings>(config.GetSection("EmailSettings"));

            services.AddScoped<IEmailService, EmailService>();

            return services;
        }

    }
}
