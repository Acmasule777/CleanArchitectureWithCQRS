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
    public record getAllDepartmentsQuery  : IRequest<List<DepartmentDto>>;

    public class getAllDepartmentHandler(IDepartment repository) : IRequestHandler<getAllDepartmentsQuery, List<DepartmentDto>>
    {
        public Task<List<DepartmentDto>> Handle(getAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            return repository.GetAllDepartment();
        }
    }
}
