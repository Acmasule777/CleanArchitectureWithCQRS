using MediatR;
using Payroll.Application.Interfaces;
using Payroll.core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Queries
{
    public record getPayrollByEmpId(int id) : IRequest<PayrollDTO>;

    public class getPayrollByEmpIdHandler(IPayroll repository) : IRequestHandler<getPayrollByEmpId, PayrollDTO>
    {
        public Task<PayrollDTO?> Handle(getPayrollByEmpId request, CancellationToken cancellationToken)
        {
            return repository.GetPayrollByEmpId(request.id);
        }
    }
}
