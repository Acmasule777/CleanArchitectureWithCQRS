using Payroll.core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Interfaces
{
    public interface IPayroll
    {
        Task<PayrollDTO?> GetPayrollById(int id);
        Task<string> CreatePayRoll(PayrollDTO payroll);
        Task<string> UpdatePayroll(PayrollDTO payroll);
        Task<string> DeletePayroll(int id);
        Task<List<PayrollDTO>> GetPayrollsByIds(List<int> ids);
        Task<PayrollDTO?> GetPayrollByEmpId(int id);
    }
}
