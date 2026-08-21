using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.core.DTOS
{
    public class PayrollDTO
    {
        [Key]
        public int PayrollId { get; set; }
        public int EmployeeId { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal Allowance { get; set; }
        public decimal Deduction { get; set; }
        public decimal Tax { get; set; }
        public decimal NetSalary { get; set; }
        public DateTime PayrollMonth { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
