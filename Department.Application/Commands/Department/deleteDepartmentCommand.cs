using Department.Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application.Commands.Department
{
    public record deleteDepartmentCommand(int id) : IRequest<string>;

    public class deleteDepartmentHandler(IDepartment repository) : IRequestHandler<deleteDepartmentCommand, string>
    {
        public Task<string> Handle(deleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            return repository.DeleteDepartment(request.id);
        }
    }
}
