using Department.Application.Commands.Department;
using Department.Application.Queries;
using DepartmentCore.Core.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Department.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Byname/{name}")]
        public async Task<ActionResult<DepartmentDto>> GetByName(string name)
        {
            var result = await _mediator.Send(new getDepartmentByName(name));
            return result is null ? NotFound() : Ok(result);
        }


        [HttpGet("GetAllDepartments")]

        public async Task<IActionResult> GetDepartments()
        {
            var result = await _mediator.Send(new getAllDepartmentsQuery());
            return Ok(result);
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetDepartmentById(int id)
        {
            var result = await _mediator.Send(new getDepartmentByIdQuery(id));
            if (result == null)
            {
                return NotFound("Department is not present in current context");
            }
            return Ok(result);
        }

        [HttpPost("batch")]
        public async Task<ActionResult<List<DepartmentDto>>> GetByIds([FromBody] List<int> ids)
        {
            var result = await _mediator.Send(new GetDepartmentsByIdsQuery(ids));
            return Ok(result);
        }

        [HttpPost("createDepeartment")]

        public async Task<ActionResult> CreateDepartment([FromBody] DepartmentDto department)
        {
            var result = await _mediator.Send(new createDepartmentCommand(department));
            return Ok(result);
        }


        [HttpPost("internal")]  // route: api/Department/internal
        public async Task<ActionResult<int>> CreateDepartmentInternal([FromBody] DepartmentDto pyload)
        {
            var newId = await _mediator.Send(new CreateDepartmentInternalCommand(pyload));
            return Ok(newId);
        }


        [HttpPut("updateDepartment")]

        public async Task<ActionResult> UpdateDepartment([FromBody] UpdateDepartmentDto department)
        {
            var result = await _mediator.Send(new updateDepartmentCommand(department));
            if (result == null)
            {
                return NotFound("Department is not present in current context");
            }
            return Ok(result);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult> DeleteDepartment(int id)
        {
            var result = await _mediator.Send(new deleteDepartmentCommand(id));
            if (result == null)
            {
                return NotFound("Department is not present in current context");
            }
            return Ok(result);
        }
    }
}
