using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentCore.Core.Entities
{
    public class DepartmentEntity
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string? DepartmentName { get; set; }
       
    }
}
