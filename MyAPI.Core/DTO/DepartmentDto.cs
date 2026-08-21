using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Core.DTO
{
    public class DepartmentDto
    {
        [Key]
        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }
    }
}
