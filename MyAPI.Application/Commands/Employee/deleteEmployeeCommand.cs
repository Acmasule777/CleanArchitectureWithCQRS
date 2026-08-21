using MediatR;
using MyAPI.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Commands.Employee
{
    public record deleteEmployeeCommand(int id) : IRequest<string>;

    public class deleteEmployeeHandler(IEmployee repository) : IRequestHandler<deleteEmployeeCommand, string>
    {
        public Task<string> Handle(deleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            return repository.DeleteEmployee(request.id);
        }
    }
}
