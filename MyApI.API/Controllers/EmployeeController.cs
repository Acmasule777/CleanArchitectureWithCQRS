using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyAPI.Application.Commands.EmployeeCommand;
using MyAPI.Application.Queries.Employee;
using MyAPI.Core.DTO;

namespace MyApI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllEmployees")]

        public async Task<ActionResult<List<EmployeeDto>>> getAllEmployees()
        {
            var result = await _mediator.Send(new getAllEmployeesQuery());
            return Ok(result);
        }

        [Authorize(Roles = "User")]
        [HttpGet("{id}")]

        public async Task<ActionResult<EmployeeDto>> getEmployeeById(int id)
        {
            var result = await _mediator.Send(new getEmployeeByIdQuery(id));
            if (result == null)
            {
                return NotFound("Employee is not present in current context");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("CreateEmployee")]
        public async Task<IActionResult> createEmployee([FromBody] EmployeeDto employee)
        {
            var result = await _mediator.Send(new createEmployeeCommand(
                employee.Name,
                employee.City,
                employee.Email,
                employee.DepartmentName
               ));
            return Ok(result);
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateEmployee")]

        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeUpdateDto employee)
        {
            var result = await _mediator.Send(new updateEmployeeCommand(employee));
            if (result == null)
            {
                return NotFound("Employee is not present in current context");
            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]

        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var result = await _mediator.Send(new deleteEmployeeCommand(id));
            if (result == null)
            {
                return NotFound("Employee is not present in current context");
            }
            return Ok(result);
        }

    }
}
