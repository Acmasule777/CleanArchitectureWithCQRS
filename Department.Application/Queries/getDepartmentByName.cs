using Department.Application.Interfaces;
using DepartmentCore.Core.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application.Queries
{
    public record getDepartmentByName (string name) : IRequest<DepartmentDto>;

    public class getDepartmentHandler(IDepartment repository) : IRequestHandler<getDepartmentByName, DepartmentDto>
    {
        public async Task<DepartmentDto> Handle(getDepartmentByName request, CancellationToken cancellationToken)
        {
            return await repository.GetByNameAsync(request.name);
        }
    }
}
