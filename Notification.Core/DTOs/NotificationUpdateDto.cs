using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notification.Core.DTOs
{
    public class NotificationUpdateDto
    {
        [Key]
        public int NotificationId { get; set; }
        public string NotificationName { get; set; }

        public string Recipient { get; set; }
        public string NotificationType { get; set; }

        public string message { get; set; }
    }
}
