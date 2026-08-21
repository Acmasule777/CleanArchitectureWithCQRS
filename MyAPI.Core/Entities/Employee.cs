using System.ComponentModel.DataAnnotations;

namespace MyAPI.Core.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? City { get; set; }

        public int DepartmentId { get; set; }
    }
}
