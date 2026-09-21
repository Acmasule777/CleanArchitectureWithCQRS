using AuthService.Application.Commands;
using AuthService.Core.DTOs;
using AuthService.Core.Entity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace AuthService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthServiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("RegisterUser")]

        public async Task<ActionResult> RegisterUser([FromBody] UserDTO user)
        {
            var result = await _mediator.Send(new registeredUserCommand(user.Username, user.PasswordHash, user.Role));

            return Ok(result);
        }

        [HttpPost("Login")]

        public async Task<IActionResult> Login([FromBody] UserDTO user)
        {
            var result = await _mediator.Send(new loginCommand(user.Username, user.PasswordHash));
            return Ok(result);
        }
    }
}
