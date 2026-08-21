using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Commands.Employee
{
    public record createEmployeeCommand (string Name, string City, string DepartmentName) : IRequest<string>;
    public class createCommandHandler(IEmployee Emprepository, IDepartmentServiceClient DmpRepository ) : IRequestHandler<createEmployeeCommand, string>
    {

        public async Task<string> Handle(createEmployeeCommand request, CancellationToken cancellationToken)
        {
            var existingDepartment = await DmpRepository.GetDepartmentByNameAsync(request.DepartmentName);

            int departmentId;

            if (existingDepartment is not null)
            {
                // Found it — reuse the existing Id
                departmentId = existingDepartment.DepartmentId;
            }
            else
            {
                // Not found — create it, get back the new Id
                departmentId = await DmpRepository.CreateDepartmentAsync(request.DepartmentName);
            }

            // Step 2: save the employee with the resolved DepartmentId (never the name)
            var employee = new EmployeeDto
            {
                Name = request.Name,
                City = request.City,
                DepartmentId = departmentId
            };


            return await Emprepository.AddEmployee(employee);
        }
    }

}
