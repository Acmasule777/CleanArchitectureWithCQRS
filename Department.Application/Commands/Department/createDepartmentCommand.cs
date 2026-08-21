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
    public record createDepartmentCommand(DepartmentDto Dto) : IRequest<string>;

    public class createDepartmentHandler(IDepartment repository) : IRequestHandler<createDepartmentCommand, string>
    {
        public Task<string> Handle(createDepartmentCommand request, CancellationToken cancellationToken)
        {
            return repository.CreateDepartment(request.Dto);
        }
    }
}
