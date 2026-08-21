using MediatR;
using Payroll.Application.Interfaces;
using Payroll.core.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Commands
{
    public record updatePayrollCommand (PayrollDTO dto) :IRequest<string>;

    public class updatePayrollHandler(IPayroll repository) : IRequestHandler<updatePayrollCommand, string>
    {
        public Task<string> Handle(updatePayrollCommand request, CancellationToken cancellationToken)
        {
            return repository.UpdatePayroll(request.dto);
        }
    }
}
