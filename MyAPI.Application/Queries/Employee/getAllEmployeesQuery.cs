using MediatR;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;


namespace MyAPI.Application.Queries.Employee
{
    public record getAllEmployeesQuery : IRequest<List<EmployeeDto>>;

    public class getAllEmployeeHandler(IEmployee EmpRepository, IDepartmentServiceClient DepRepository, IPayrollRepositoryClient prollRepository) : IRequestHandler<getAllEmployeesQuery, List<EmployeeDto>>
    {
        public async Task<List<EmployeeDto>> Handle(getAllEmployeesQuery request, CancellationToken cancellationToken)
        {
            var employees = await EmpRepository.GetAllEmployees();

            var departmentIds = employees.Select(e => e.DepartmentId).Distinct().ToList();

            var departments = await DepRepository.GetDepartmentsByIdsAsync(departmentIds);

            var DepartmentLookup = departments.ToDictionary(d => d.DepartmentId, d => d.DepartmentName);

            var EmployeeIdsFromPayrolls = employees.Select(e => e.Id).ToList();

            var Payrolls = await prollRepository.GetAllPayrollsById(EmployeeIdsFromPayrolls);



            return employees.Select(e => new EmployeeDto {
                Id = e.Id,
                Name = e.Name,
                City = e.City,
                DepartmentId = e.DepartmentId,
                DepartmentName = DepartmentLookup.TryGetValue(e.DepartmentId, out var name) ? name : "Unknown",
                  payroll = Payrolls
                  .Where(p => p.EmployeeId == e.Id)
                  .Select(p => new PayrollServiceDto
                  {
                      PayrollId = p.PayrollId,
                      EmployeeId = p.EmployeeId,
                      Allowance = p.Allowance,
                      BasicSalary = p.BasicSalary,
                      Deduction = p.Deduction,
                      Tax = p.Tax,
                      NetSalary = p.NetSalary,
                      CreatedAt = p.CreatedAt,
                      PayrollMonth = p.PayrollMonth
                  }).FirstOrDefault()
            }).ToList();
        }
    }
}
