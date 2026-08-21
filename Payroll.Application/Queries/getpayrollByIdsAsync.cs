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
    public record getpayrollByIdsAsync(List<int> ids) : IRequest<List<PayrollDTO>>;

    public class getpayrollByIdsHanderQuery(IPayroll repository) : IRequestHandler<getpayrollByIdsAsync, List<PayrollDTO>>
    {
        public Task<List<PayrollDTO>> Handle(getpayrollByIdsAsync request, CancellationToken cancellationToken)
        {
            return repository.GetPayrollsByIds(request.ids);
        }
    }
}
