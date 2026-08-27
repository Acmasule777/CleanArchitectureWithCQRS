using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class GetDepartmentResponse
    {
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public string? CorrelationId { get; set; }
    }
}
