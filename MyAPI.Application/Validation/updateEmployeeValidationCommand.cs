using FluentValidation;
using MyAPI.Application.Commands.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Validation
{
    public class updateEmployeeValidationCommand : AbstractValidator<updateEmployeeCommand>
    {
        public updateEmployeeValidationCommand()
        {
            RuleFor(e => e.Dto.Name).MaximumLength(50).WithMessage("You should enter employee name below 50 characters");
            RuleFor(e => e.Dto.City).MaximumLength(30).WithMessage("City name character should be below 30");
        }
    }
}
