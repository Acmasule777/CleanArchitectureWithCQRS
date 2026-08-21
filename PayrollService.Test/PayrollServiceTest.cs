using Payroll.Application.Commands;
using Payroll.Application.Validations;
using Payroll.core.DTOS;
using PayrollService.Test;
using System;
using FluentValidation.Results;
using Payroll.Application.Commands;
using Payroll.Application.Validations;
using Payroll.core.DTOS;
using Xunit;

namespace PayrollService.Test
{
    public class PayrollServiceTest
    {
        private readonly createPayrollValidationCommand _validator = new();

        [Theory]
        [InlineData(0, 10, 1000, 1, 100, false)]      // Allowance = 0 -> invalid
        [InlineData(100, 500, 1000, 1, 100, false)]   // Deduction = 500 -> invalid (must be <500)
        [InlineData(100, 10, 0, 1, 100, false)]       // BasicSalary = 0 -> invalid
        [InlineData(100, 10, 1000, 0, 100, false)]    // PayrollMonth = DateTime.MinValue -> invalid
        [InlineData(100, 10, 1000, 1, 0, false)]      // NetSalary = 0 -> invalid
        [InlineData(100, 10, 1000, 1, 100, true)]     // All valid
        public void CreatePayrollValidator_ValidatesBusinessRules(decimal allowance, decimal deduction, decimal basicSalary, int payrollMonthMonthValue, decimal netSalary, bool expectValid)
        {
            // Arrange
            var dto = new PayrollDTO
            {
                Allowance = allowance,
                Deduction = deduction,
                BasicSalary = basicSalary,
                PayrollMonth = payrollMonthMonthValue == 0 ? DateTime.MinValue : new DateTime(2023, payrollMonthMonthValue, 1),
                NetSalary = netSalary
            };

            var command = new createPayrollCommand(dto);

            // Act
            ValidationResult result = _validator.Validate(command);

            // Assert
            Assert.Equal(expectValid, result.IsValid);
        }


    }
}