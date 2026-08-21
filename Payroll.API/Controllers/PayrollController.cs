using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Payroll.Application.Commands;
using Payroll.Application.Queries;
using Payroll.core.DTOS;

namespace Payroll.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PayrollController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PayrollController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Byid/{id}")]

        public async Task<ActionResult> GetPayrollById(int id)
        {
            var result = await _mediator.Send(new getPayrollByIdQuery(id));
            return Ok(result);
        }

        [HttpGet("ByEmpId/{id}")]

        public async Task<ActionResult> GetPayrollByEmpId(int id)
        {
            var result = await _mediator.Send(new getPayrollByEmpId(id));
            return Ok(result);
        }

        [HttpPost("CreatePayroll")]

        public async Task<IActionResult> CreatePayroll([FromBody] PayrollDTO payroll)
        {
            var result = await _mediator.Send(new createPayrollCommand(payroll));
            return Ok(result);
        }

        [HttpPut("updatePayroll")]

        public async Task<IActionResult> UpdatePayroll([FromBody] PayrollDTO payroll)
        {
            var result = await _mediator.Send(new updatePayrollCommand(payroll));
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<IActionResult> DeletePayroll(int id)
        {
            var result = await _mediator.Send(new deletePayrollCommand(id));
            return Ok(result);
        }

        [HttpPost("payrollBatch")]
        public async Task<ActionResult<List<PayrollDTO>>> GetAllPayrollsByIds([FromBody] List<int> ids)
        {
            var result = await _mediator.Send(new getpayrollByIdsAsync(ids));
            return Ok(result);
        }
    }
}
