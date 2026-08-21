using Microsoft.EntityFrameworkCore;
using Payroll.Application.Interfaces;
using Payroll.core.DTOS;
using Payroll.core.Entities;
using Payroll.Infrastructure.Persistancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Infrastructure.Repositories
{
    public class PayrollRepository : IPayroll
    {
        private readonly AppPayrollDbContext _context;

        public PayrollRepository(AppPayrollDbContext context)
        {
            _context = context;
        }

        public async Task<PayrollDTO?> GetPayrollById(int id)
        {
            return await _context.Payrolls
           .Where(p => p.PayrollId == id)
           .Select(p => new PayrollDTO
           {
               PayrollId = p.PayrollId,
               EmployeeId = p.EmployeeId,
               Allowance = p.Allowance,
               BasicSalary = p.BasicSalary,
               Deduction = p.Deduction,
               Tax = p.Tax,
               NetSalary = p.NetSalary,
               PayrollMonth = p.PayrollMonth,
               CreatedAt = p.CreatedAt
           }).FirstOrDefaultAsync();
        }

        public async Task<string> CreatePayRoll(PayrollDTO payroll)
        {
            await _context.Payrolls.AddAsync(new PayrollEntity
            {
                EmployeeId = payroll.EmployeeId,
                Allowance = payroll.Allowance,
                BasicSalary = payroll.BasicSalary,
                Deduction = payroll.Deduction,
                Tax = payroll.Tax,
                NetSalary = payroll.NetSalary,
                PayrollMonth = payroll.PayrollMonth,
                CreatedAt = DateTime.Now
            });
            await _context.SaveChangesAsync();
            return "Payroll Successfully Created";
        }

        public async Task<string> UpdatePayroll(PayrollDTO payroll)
        {
            var OldPayroll = await _context.Payrolls.FindAsync(payroll.PayrollId);

            OldPayroll.EmployeeId = payroll.EmployeeId != 0 ? payroll.EmployeeId : OldPayroll.EmployeeId;
            OldPayroll.Allowance = payroll.Allowance != 0 ? payroll.Allowance : OldPayroll.Allowance;
            OldPayroll.BasicSalary = payroll.BasicSalary != 0 ? payroll.BasicSalary : OldPayroll.BasicSalary;
            OldPayroll.Deduction = payroll.Deduction != 0 ? payroll.Deduction : OldPayroll.Deduction;
            OldPayroll.Tax = payroll.Tax != 0 ? payroll.Tax : OldPayroll.Tax;
            OldPayroll.NetSalary = payroll.NetSalary != 0 ? payroll.NetSalary : OldPayroll.NetSalary;
            OldPayroll.PayrollMonth = payroll.PayrollMonth != DateTime.MinValue ? payroll.PayrollMonth : OldPayroll.PayrollMonth;

            _context.Payrolls.Update(OldPayroll);
            await _context.SaveChangesAsync();
            return "Payroll successfully updated";
        }

        public async Task<string> DeletePayroll(int id)
        {
            var payroll = await _context.Payrolls.FindAsync(id);
            _context.Payrolls.Remove(payroll);
            await _context.SaveChangesAsync();
            return "Payroll Successfully Deleted";
        }

        public async Task<List<PayrollDTO>> GetPayrollsByIds(List<int> ids)
        {
            return await _context.Payrolls
                .Where(payroll => ids.Contains(payroll.EmployeeId))
                .Select(payroll => new PayrollDTO
                {
                    PayrollId = payroll.PayrollId,
                    EmployeeId = payroll.EmployeeId,
                    Allowance = payroll.Allowance,
                    BasicSalary = payroll.BasicSalary,
                    Deduction = payroll.Deduction,
                    Tax = payroll.Tax,
                    NetSalary = payroll.NetSalary,
                    PayrollMonth = payroll.PayrollMonth,
                    CreatedAt = DateTime.Now
                }).ToListAsync();

        }

        public async Task<PayrollDTO?> GetPayrollByEmpId(int id)
        {
            return await _context.Payrolls
                .Where(Payroll => Payroll.EmployeeId == id)
                .Select(payroll => new PayrollDTO
                {
                    PayrollId = payroll.PayrollId,
                    EmployeeId = payroll.EmployeeId,
                    Allowance = payroll.Allowance,
                    BasicSalary = payroll.BasicSalary,
                    Deduction = payroll.Deduction,
                    Tax = payroll.Tax,
                    NetSalary = payroll.NetSalary,
                    PayrollMonth = payroll.PayrollMonth,
                    CreatedAt = DateTime.Now
                }).FirstOrDefaultAsync();
        }
    }
}
