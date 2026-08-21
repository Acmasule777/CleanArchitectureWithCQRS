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
    public record createPayrollCommand(PayrollDTO dto) : IRequest<string>;

    public class createPayrollHander(IPayroll repository) : IRequestHandler<createPayrollCommand, string>
    {
        public Task<string> Handle(createPayrollCommand request, CancellationToken cancellationToken)
        {
            return repository.CreatePayRoll(request.dto);
        }
    }
}
