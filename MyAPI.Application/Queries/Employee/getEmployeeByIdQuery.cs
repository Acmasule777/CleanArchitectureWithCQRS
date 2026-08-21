using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Queries.Employee
{
    public record getEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;

    public class getEmployeeByIdQueryHandler(IEmployee repository, IDepartmentServiceClient DmpRepository, IPayrollRepositoryClient payrollrepository) : IRequestHandler<getEmployeeByIdQuery, EmployeeDto>
    {
        public async Task<EmployeeDto> Handle(getEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await repository.GetEmployeeById(request.Id);

            if (employee is null)
                return employee;

            var department = await DmpRepository.GetDepartmentById(employee.DepartmentId);

            var payroll = await payrollrepository.GetPayrollById(employee.Id);



            return new EmployeeDto
            {
                Id = employee.Id,
                Name = employee.Name,
                City = employee.City,
                DepartmentId = employee.DepartmentId,
                DepartmentName = department is null ? "Unknown" : department.DepartmentName,
                payroll = payroll is null ? null : new PayrollServiceDto
                {
                    PayrollId = payroll.PayrollId,
                    EmployeeId = payroll.EmployeeId,
                    Allowance = payroll.Allowance,
                    BasicSalary = payroll.BasicSalary,
                    Deduction = payroll.Deduction,
                    NetSalary = payroll.NetSalary,
                    CreatedAt = payroll.CreatedAt,
                    PayrollMonth = payroll.PayrollMonth
                }
            };
        }
    }
}
