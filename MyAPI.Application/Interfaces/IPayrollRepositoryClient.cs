using MyAPI.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyAPI.Application.Interfaces
{
    public interface IPayrollRepositoryClient
    {
        Task<PayrollServiceDto?> GetPayrollById(int empId);
        Task<List<PayrollServiceDto>> GetAllPayrollsById(List<int> ids);
    }
}
