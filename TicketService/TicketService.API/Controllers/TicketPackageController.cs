using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TicketService.Application.Command;
using TicketService.Application.DTOs.Request;
using TicketService.Application.Queries;

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
        [SwaggerOperation(Summary = "Tạo ticket-package")]
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

        [HttpGet("ticket-packages")]
        [SwaggerOperation(Summary = "Lấy hết tất cả ticket-package")]
        public async Task<IActionResult> GetAllTicketPackage()
        {
            var result = await _mediator.Send(new GetAllTicketPackageQuery());
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No ticket packages found." });
            }
            return Ok(result);
        }
        [HttpGet("ticket-packages-active")]
        [SwaggerOperation(Summary = "Lấy tất cả ticket-package có status = ACTIVE")]

        public async Task<IActionResult> GetAllActiveTicketPackage()
        {
            var result = await _mediator.Send(new GetAllTicketPackageActiveQuery());
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No active ticket packages found." });
            }
            return Ok(result);
        }
        [HttpGet("ticket-package/{id}")]
        [SwaggerOperation(Summary = "Lấy ticket-package dựa trên ticket-package-id")]

        public async Task<IActionResult> GetTicketPackageById(string id)
        {
            var result = await _mediator.Send(new GetTicketPackageByIdQuery(Guid.Parse(id)));
            if (result == null)
            {
                return NotFound(new { message = "Ticket package not found." });
            }
            return Ok(result);
        }

        [HttpGet("ticket-package")]
        [SwaggerOperation(Summary = "Lấy tất cả ticket-package có phân trang")]

        public async Task<IActionResult> GetTicketPackagesPagition([FromQuery] int page)
        {
            var result = await _mediator.Send(new GetTicketPackagesPagitionQuery(page));
            if (result == null || !result.Any())
            {
                return NotFound(new { message = "No ticket packages found." });
            }
            return Ok(result);
        }

        [HttpPut("ticket-package/{id}")]
        [SwaggerOperation(Summary = "Update ticket-package")]

        public async Task<IActionResult> UpdateTicketPackage(string id, [FromBody] UpdateTicketPackageRequest request)
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

            var command = new UpdateTicketPackageCommand(Guid.Parse(id), request.TicketPackageName, request.RequestAmount, request.Price, request.Status);

            var result = await _mediator.Send(command);
            return Ok(result);
        }
        [HttpDelete("ticket-package/{id}")]
        [SwaggerOperation(Summary = "Soft delete ticket-package")]

        public async Task<IActionResult> DeleteTicketPackage(string id)
        {
            var command = new DeleteTicketPackageCommand(Guid.Parse(id));
           var result =  await _mediator.Send(command);
            return Ok(result);
        }
    }
}
