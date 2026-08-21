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
    public record getPayrollByIdQuery(int id) : IRequest<PayrollDTO>;


    public class getPayrollByIdHandler(IPayroll repository) : IRequestHandler<getPayrollByIdQuery, PayrollDTO>
    {
        public Task<PayrollDTO?> Handle(getPayrollByIdQuery request, CancellationToken cancellationToken)
        {
            return repository.GetPayrollById(request.id);
        }
    }
}
