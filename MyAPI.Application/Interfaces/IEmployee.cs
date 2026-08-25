using MyAPI.Core.DTO;
using MyAPI.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Interfaces
{
    public interface IEmployee
    {
        Task<List<EmployeeDto>> GetAllEmployees();
        Task<EmployeeDto> GetEmployeeById(int id);
        Task<string> UpdateEmployee(EmployeeUpdateDto employee);
        Task<string> DeleteEmployee(int id);
        Task<int> AddEmployee(EmployeeDto dto);
    }
}
