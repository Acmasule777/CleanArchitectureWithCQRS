using FluentValidation;
using Payroll.Application.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Validations
{
    public class createPayrollValidationCommand : AbstractValidator<createPayrollCommand>
    {
        public createPayrollValidationCommand()
        {
            RuleFor(p => p.dto.Allowance).GreaterThan(0).WithMessage("Allowance should be greaterthan zero and even not equal to zero");
            RuleFor(p => p.dto.Deduction).LessThan(500).WithMessage("Deduction should be lessthan 500");
            RuleFor(p => p.dto.BasicSalary).GreaterThan(0).NotEmpty().WithMessage("Basic salary should be greater that zero and even not equal to zero");
            RuleFor(p => p.dto.PayrollMonth).NotEmpty().WithMessage("PayrollMonth should not empty");
            RuleFor(p => p.dto.NetSalary).GreaterThan(0).NotNull().WithMessage("NetSalary should be greater that zero and even not equal to zero");
        }
    }
}
