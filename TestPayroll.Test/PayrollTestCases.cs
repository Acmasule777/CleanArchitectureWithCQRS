using Payroll.Application.Commands;
using Payroll.Application.Validations;
using Payroll.core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPayroll.Test
{
    public class PayrollTestCases
    {
        private readonly createPayrollValidationCommand _validator = new();

        [Fact]
        public void createPaylollHandle()
        {
            var command = new createPayrollCommand(new PayrollDTO
            {
                EmployeeId = 1,
                Allowance = 2000,
                BasicSalary = 0,
                Deduction = 200,
                Tax = 500,
                NetSalary = 20000,
                PayrollMonth = new DateTime(2026, 8, 30),
                CreatedAt = DateTime.Now
            });

            var result = _validator.Validate(command);

            Assert.False(result.IsValid);
            Assert.Contains(result.Errors, e => e.PropertyName == "dto.BasicSalary");


        }
    }
}
