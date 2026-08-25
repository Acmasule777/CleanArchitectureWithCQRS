using FluentValidation;
using MyAPI.Application.Commands.EmployeeCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Validation
{
    public class createEmployeeCommandValidations : AbstractValidator<createEmployeeCommand>
    {
        public createEmployeeCommandValidations()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithMessage("Employee name should not be Empty")
                .NotNull().WithMessage("Employee name should not be Null")
                .MaximumLength(50).WithMessage("Employee name should be below 50 characters");

            RuleFor(e => e.City)
                .NotEmpty().WithMessage("City should not be Empty")
                .NotNull().WithMessage("City should not be Null")
                .MaximumLength(30).WithMessage("City name should be below 30 characters");

        }
    }
}
