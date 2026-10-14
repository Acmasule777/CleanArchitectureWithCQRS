using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Core.Entities
{
    public class IdempotencyEntity
    {
        public int Id { get; set; }

        public string Key { get; set; } = string.Empty;

        public string RequestHash { get; set; } = string.Empty;

        public string Status { get; set; } = "Processing";

        public string? ResponseBody { get; set; }

        public DateTime? CompletedAtUtc { get; set; }
    }
}
