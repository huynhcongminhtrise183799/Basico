using BookingService.Application.Command;
using BookingService.Application.DTOs.Request;
using BookingService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingService.API.Controllers
{
	[Route("api/")]
	[ApiController]
	public class FeedbackController : ControllerBase
	{
		private readonly IMediator _mediator;

		public FeedbackController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("feedback")]
		public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
		{
			var command = new CreateFeedbackCommand(request.BookingId, request.CustomerId, request.FeedbackContent, request.Rating);
			var result = await _mediator.Send(command);
			return Ok(result);
		}

		[HttpPut("feedback/{id}")]
		public async Task<IActionResult> UpdateFeedback(string id,[FromBody] UpdateFeedbackRequest request)
		{
			try
			{
				var command = new UpdateFeedbackCommand(Guid.Parse(id), request.FeedbackContent, request.Rating);
				var result = await _mediator.Send(command);
				if (result)
				{
					return Ok(new
					{
						message = "Update successfully"
					});
				}
				return Ok(new
				{
					message = "Have some error"
				});
			}
			catch (InvalidOperationException ex)
			{
				return NotFound(new { message = ex.Message });
			}

		}

		[HttpGet("feedbacks")]
		public async Task<IActionResult> AllFeedbacks()
		{
			var query = new GetAllFeedbackQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("feedback/{id}")]
		public async Task<IActionResult> DetailFeedback(string id)
		{
			try
			{
				var query = new GetDetailFeedbackQuery(Guid.Parse(id));
				var result = await _mediator.Send(query);
				return Ok(result);
			}
			catch (InvalidOperationException ex)
			{
				return NotFound(new { message = ex.Message });
			}
		}

		[HttpGet("feedback/booking/{id}")]
		public async Task<IActionResult> GetFeedbackByBookingId(string id)
		{
			var query = new GetFeedbackByBookingIdQuery(Guid.Parse(id));
			var result = await _mediator.Send(query);
			if(result == null)
			{
				return Ok(new
				{
					message = "Booking doesn't have feedback"
				});
			}
			return Ok(result);
		}
	}
}
