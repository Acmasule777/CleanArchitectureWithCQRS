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
    public record GetDepartmentsByIdsQuery(List<int> ids) : IRequest<List<DepartmentDto>>;

    public class GetDepartmentsByIdsHandler(IDepartment repository) : IRequestHandler<GetDepartmentsByIdsQuery, List<DepartmentDto>>
    {
        public Task<List<DepartmentDto>> Handle(GetDepartmentsByIdsQuery request, CancellationToken cancellationToken)
        {
            return repository.GetDepartmentByIds(request.ids);
        }
    }
}
