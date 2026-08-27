using DepartmentCore.Core.DTOs;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Department.Application.Interfaces
{
    public interface IDepartment
    {
        Task<List<DepartmentDto>> GetAllDepartment();
        Task<DepartmentDto?> GetDepartmentById(int id);
        Task<List<DepartmentDto>> GetDepartmentByIds(List<int> ids);
        Task<string> CreateDepartment(DepartmentDto department);
        Task<string> UpdateDepartment(UpdateDepartmentDto department);
        Task<string> DeleteDepartment(int id);

        Task<DepartmentDto?> GetByNameAsync2(GetDepartmentRequest request);

        Task<DepartmentDto?> GetByNameAsync(string name);



        Task<int> AddAsync(DepartmentDto department);
    }
}
