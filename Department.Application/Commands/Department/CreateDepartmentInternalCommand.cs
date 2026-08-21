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
    public record CreateDepartmentInternalCommand(DepartmentDto payload) : IRequest<int>;

    public class CreateDepartmentInternalHandler(IDepartment repository)
    : IRequestHandler<CreateDepartmentInternalCommand, int>
    {
        public async Task<int> Handle(CreateDepartmentInternalCommand request, CancellationToken ct)
        {
            //var department = new DepartmentDto
            //{
            //    DepartmentName = request.DepartmentName
            //};

            var result = await repository.AddAsync(request.payload); // adjust to your actual repo method name
            return result;          // return just the new Id
        }
    }

}
