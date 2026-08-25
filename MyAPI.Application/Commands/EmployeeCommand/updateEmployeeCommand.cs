using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Commands.EmployeeCommand
{
    public record updateEmployeeCommand (EmployeeUpdateDto Dto) : IRequest<string>;

    public class updateEmployeeHandler(IEmployee repository) : IRequestHandler<updateEmployeeCommand, string>
    {
        public Task<string> Handle(updateEmployeeCommand request, CancellationToken cancellationToken)
        {
            return repository.UpdateEmployee(request.Dto);
        }
    }
}
