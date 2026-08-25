using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyAPI.Core.DTO
{
    public class EmployeeUpdateDto
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        
        public string? Email { get; set; }
    }
}
