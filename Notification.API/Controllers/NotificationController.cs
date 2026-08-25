using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nofication.Application.Queries;
using Notification.Core.DTOs;

namespace Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetNotification")]

        public async Task<IActionResult> SendEmailNotification([FromBody] NotificationDTO notification)
        {
            var result = await _mediator.Send(new getNotificationQuery(notification.Recipient,notification.message));

            return Ok("Email Successfully Sent");
        }
    }
}
