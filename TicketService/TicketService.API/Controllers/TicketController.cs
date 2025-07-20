using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TicketService.Application.Command.Ticket;
using TicketService.Application.Handler.QueryHandler.TicketQueryHandler;
using TicketService.Application.Queries;

namespace TicketService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketCommand command)
        {
            var ticketId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTicket), new { id = ticketId }, new { TicketId = ticketId });
        }

        [HttpPut("{id}/reply")]
        public async Task<IActionResult> ReplyTicket(Guid id, [FromBody] ReplyTicketCommand command)
        {
            if (id != command.TicketId) return BadRequest("ID mismatch");

            var success = await _mediator.Send(command);
            if (!success) return NotFound();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(Guid id)
        {
            var success = await _mediator.Send(new DeleteTicketCommand { TicketId = id });
            if (!success) return NotFound();

            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket(Guid id)
        {
            var query = new GetTicketByTicketIdQuery(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllTickets()
        {
            var tickets = await _mediator.Send(new GetAllTicketsQuery());
            return Ok(tickets);
        }

        [HttpGet("by-status")]
        public async Task<IActionResult> GetByStatus([FromQuery] string status)
        {
            var tickets = await _mediator.Send(new GetTicketsByStatusQuery { Status = status });
            return Ok(tickets);
        }

        [HttpGet("by-customer")]
        public async Task<IActionResult> GetByCustomerId([FromQuery] Guid userId)
        {
            var tickets = await _mediator.Send(new GetTicketsByCustomerIdQueryService { UserId = userId });
            return Ok(tickets);
        }
    }
}
