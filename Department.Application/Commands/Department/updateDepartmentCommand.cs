using Department.Application.Interfaces;
using DepartmentCore.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application.Commands.Department
{
    public record updateDepartmentCommand(UpdateDepartmentDto Dto) : IRequest<string>;

    public class updateDepartmentHandler(IDepartment repository) : IRequestHandler<updateDepartmentCommand, string>
    {
        public Task<string> Handle(updateDepartmentCommand request, CancellationToken cancellationToken)
        {
            return repository.UpdateDepartment(request.Dto);
        }
    }
}
