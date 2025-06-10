using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketService.Application.Command;
using TicketService.Application.DTOs.Request;

namespace TicketService.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TicketPackageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketPackageController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("ticket-package")]
        public async Task<IActionResult> CreateTicketPackage([FromBody] CreateTicketPackageRequest request)
        {
            if (!ModelState.IsValid)
            {
                var firstError = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .FirstOrDefault();

                return BadRequest(new
                {
                    message = firstError
                });

            }

            var command = new CreateTicketPackageCommand(request.TicketPackageName, request.RequestAmount, request.Price);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
