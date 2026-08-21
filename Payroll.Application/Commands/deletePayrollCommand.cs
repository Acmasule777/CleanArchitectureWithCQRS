using MediatR;
using Payroll.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Payroll.Application.Commands
{
    public record deletePayrollCommand(int id) : IRequest<string>;

    public class deletePayrollHandler(IPayroll repository) : IRequestHandler<deletePayrollCommand, string>
    {
        public Task<string> Handle(deletePayrollCommand request, CancellationToken cancellationToken)
        {
            return repository.DeletePayroll(request.id);
        }
    }
}
