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
    public record getDepartmentByIdQuery(int Id) : IRequest<DepartmentDto>;

    public class getDepartmentByIdQueryHandler(IDepartment repository) : IRequestHandler<getDepartmentByIdQuery, DepartmentDto>
    {
        public Task<DepartmentDto> Handle(getDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetDepartmentById(request.Id);
        }
    }
}
