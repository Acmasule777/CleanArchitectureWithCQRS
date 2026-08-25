using Microsoft.EntityFrameworkCore;
using Notification.Core.NotificationEntity;
using Notification.Core.Opetions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Infrastructure.Persistency
{
    public class ApplicationNotificationDbContex : DbContext
    {
        public ApplicationNotificationDbContex(DbContextOptions<ApplicationNotificationDbContex> options) : base(options) { }


        public DbSet<NotificationEntity> Notifications{ get; set; } 
    }

}
