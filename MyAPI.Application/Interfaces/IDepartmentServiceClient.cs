using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Interfaces
{
    public interface IDepartmentServiceClient
    {
        Task<DepartmentDto?> GetDepartmentById(int departmentId);
        Task<List<DepartmentDto>> GetDepartmentsByIdsAsync(List<int> ids);

        Task<DepartmentDto?> GetDepartmentByNameAsync(string name);
        Task<int> CreateDepartmentAsync(string name);
    }
}
