using Azure.Messaging;
using Microsoft.EntityFrameworkCore;
using MyAPI.Application.Interfaces;
using MyAPI.Core.DTO;
using MyAPI.Core.Entities;
using MyAPI.Infrastructure.Persistancy;

namespace MyAPI.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployee
    {
        private readonly ApplicationDbContext _context;
        private readonly IDepartmentServiceClient _departmentServiceClient;
        public EmployeeRepository(ApplicationDbContext context, IDepartmentServiceClient departmentServiceClient)
        {
            _context = context;
            _departmentServiceClient = departmentServiceClient;
        }

        public async Task<List<EmployeeDto>> GetAllEmployees()
        {
            
            return await _context.Employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                City = e.City,
                DepartmentId = e.DepartmentId,
            }).ToListAsync();
        }

        public async Task<EmployeeDto?> GetEmployeeById(int id)
        {
            return await _context.Employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                Name = e.Name,
                City = e.City,
                DepartmentId=e.DepartmentId,
            }).FirstOrDefaultAsync(e => e.Id == id);

        }


        public async Task<string> AddEmployee(EmployeeDto employee)
        {
           _context.Employees.Add(new Employee
            {
                Name = employee.Name,
                City = employee.City,
                DepartmentId = employee.DepartmentId
            });
            await _context.SaveChangesAsync();
            return "Employee added successfully";
        }

        public async Task<string> UpdateEmployee(EmployeeUpdateDto employee)
        {
            var emp = await _context.Employees.FindAsync(employee.Id);

            emp.Name = string.IsNullOrWhiteSpace(employee.Name) ? emp.Name = emp.Name : emp.Name = employee.Name;
            emp.City = string.IsNullOrWhiteSpace(employee.City) ? emp.City = emp.City : emp.City = employee.City;

            _context.Employees.Update(emp);
            await _context.SaveChangesAsync();
            return "Employee Updated Successfully";

        }

       public async Task<string> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);

            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return "Employee Deleted Successfully";
        }
    }
}
