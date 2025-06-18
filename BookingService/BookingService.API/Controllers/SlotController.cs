using BookingService.Application.DTOs.Request;
using BookingService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SlotController : ControllerBase
	{
		private readonly IMediator _mediator;
		public SlotController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("free-slot")]
		[SwaggerOperation(Summary = "Lấy ra những slot trống theo lawyer và ngày ")]
		public async Task<IActionResult> GetFreeSlots([FromQuery] Guid lawyerId, [FromQuery] DateOnly date)
		{
			var result = await _mediator.Send(new GetFreeSlotsForLawyerQuery(lawyerId, date));
			return Ok(result);
		}

		[HttpGet("slot-update/{bookingId}")]
		[SwaggerOperation(Summary = "Lấy ra những slot trống dành cho update booking")]
		public async Task<IActionResult> GetFreeSlotsForUpdate(Guid bookingId ,[FromQuery] Guid lawyerId, [FromQuery] DateOnly bookingDate)
		{
			var result = await _mediator.Send(new GetFreeSlotsForUpdateQuery(bookingId,lawyerId, bookingDate));
			return Ok(result);
		}

	}
}
	