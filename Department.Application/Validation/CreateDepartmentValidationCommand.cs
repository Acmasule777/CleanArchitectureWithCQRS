using Department.Application.Commands.Department;
using FluentValidation;
using MediatR;


namespace Department.Application.Validation
{
    public class CreateDepartmentValidationCommand : AbstractValidator<createDepartmentCommand>
    {
        public CreateDepartmentValidationCommand() 
        {
            RuleFor(d => d.Dto.DepartmentName)
                .NotEmpty().WithMessage("Department Name should not be empty")
                .NotNull().WithMessage("Department Name should not be Null")
                .MaximumLength(15).WithMessage("Character length should be below 15");
        }
    }
}
