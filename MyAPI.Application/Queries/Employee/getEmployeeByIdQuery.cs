using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace MyAPI.Application.Queries.Employee
{
    public record getEmployeeByIdQuery(int Id) : IRequest<EmployeeDto>;

    public class getEmployeeByIdQueryHandler(IEmployee repository, 
        IDepartmentServiceClient DmpRepository, 
        IPayrollRepositoryClient payrollrepository, 
        IDistributedCache cache) : IRequestHandler<getEmployeeByIdQuery, EmployeeDto>
    {
        public async Task<EmployeeDto> Handle(getEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            //Create the cachekey with given Id in string 

            string cacheKey = $"employee:{request.Id}";

            //Search Cache Employee in redis container 

            var cacheEmployee = await cache.GetStringAsync(cacheKey);

            var employee = new EmployeeDto();


            //If we got the employee from cache then deserialize that into our employee dto oject and store that
            if (!string.IsNullOrEmpty(cacheEmployee))
            {
                employee = JsonSerializer.Deserialize<EmployeeDto>(cacheEmployee);
            }
            else
            {
                employee = await repository.GetEmployeeById(request.Id);

                //If we don't have that employee into redis cache and got from the database then first serialize that for store into redis
                var serializeEmployee = JsonSerializer.Serialize(employee);

                //Here we set that employee into redis

                await cache.SetStringAsync(
                    cacheKey,
                    serializeEmployee,
                     new DistributedCacheEntryOptions
                     {
                         AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                     });
            }

            //If employee is null from the both place then return null employee

            if (employee is null)
                return employee;

            var department = await DmpRepository.GetDepartmentById(employee.DepartmentId);

            var payroll = await payrollrepository.GetPayrollById(employee.Id);

            //And return the employee here

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
