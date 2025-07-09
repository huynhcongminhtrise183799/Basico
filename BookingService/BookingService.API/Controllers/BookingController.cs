using BookingService.Application.Command;
using BookingService.Application.DTOs.Request;
using BookingService.Application.Handler.CommandHandler;
using BookingService.Application.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BookingService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookingController : ControllerBase
	{
		private readonly IMediator _mediator;
		public BookingController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost]
		[SwaggerOperation(Summary = "Tạo booking")]
		public async Task<IActionResult> CreateBooking([FromBody] CreateBookingDTO request)
		{
			var command = new CreateBookingCommand(request.BookingDate,request.Description,request.Price,request.CustomerId,request.LawyerId,request.ServiceId, request.SlotId);

			var result = await _mediator.Send(command);
			if (result == null)
			{
				return NotFound("Booking could not be created.");
			}

			return Ok(result);
		}
		[HttpPut("{bookingId}")]
		[SwaggerOperation(Summary = "Update booking")]
		public async Task<IActionResult> UpdateBooking(Guid bookingId,[FromBody] UpdateBookingDTO request)
		{
			var command = new UpdateBookingCommand(bookingId,request.LawyerId,request.CustomerId,request.ServiceId,request.BookingDate,request.SlotId,request.Price, request.Description);

			var result = await _mediator.Send(command);
			if (result == null)
			{
				return NotFound("Booking could not be updated.");
			}

			return Ok(result);
		}
		[HttpDelete("{bookingId}")]
		[SwaggerOperation(Summary = "Cancel booking")]
		public async Task<IActionResult> DeleteBooking(Guid bookingId)
		{
			var command = new CancelBookingCommand(bookingId);
			var result = await _mediator.Send(command);
			if (result == null)
			{
				return NotFound("Booking could not be deleted.");
			}

			return Ok(result);
		}

		[HttpGet("{bookingId}")]
		[SwaggerOperation(Summary = "Detail booking by bookingId")]
		public async Task<IActionResult> GetBookingById(Guid bookingId)
		{
			var query = new GetBookingByIdQuery(bookingId);
			var result = await _mediator.Send(query);
			if (result == null)
			{
				return NotFound("Booking not found.");
			}

			return Ok(result);
		}

		[HttpGet]
		[SwaggerOperation(Summary = "Lấy ra những booking của customer theo status ")]
		public async Task<IActionResult> GetBookingByStatusByCustomer([FromQuery] Guid customerId, [FromQuery] string status)
		{
			var query = new GetBookingByCustomerAndStatusQuery(customerId,status);
			var result = await _mediator.Send(query);
			return Ok(result);
		}
		[HttpGet("lawyer/{lawyerId}")]
		[SwaggerOperation(Summary = "Lấy ra những booking của lawyer theo ngày và status " +
			"1. Paid" +
			"2. CheckedIn" +
			"Completed")]



		public async Task<IActionResult> GetBookingByStatusByLawyer( Guid lawyerId, [FromQuery] string status, [FromQuery] DateOnly bookingDate)
		{
			var query = new GetBookingByLawyerAndStatusQuery(lawyerId, status, bookingDate);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpGet("staff")]
		[SwaggerOperation(Summary = "Lấy tất cả booking theo ngày và status" +
			"1. Paid" +
			"2. CheckedIn" +
			"Completed")]

		public async Task<IActionResult> GetAllBookingByStatusInDay([FromQuery] string status, [FromQuery] DateOnly bookingDate)
		{
			var query = new GetAllBookingInDayQuery(bookingDate, status);
			var result = await _mediator.Send(query);
			return Ok(result);
		}

		[HttpPut("check-in/{bookingId}")]
		[SwaggerOperation(Summary = "Check in booking")]
		public async Task<IActionResult> CheckInBooking(Guid bookingId)
		{
			var command = new CheckInBookingCommand(bookingId);
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound("Booking could not be checked in.");
			}

			return Ok(new
			{
				message = "Booking checked in successfully."
			});
		}
		[HttpPut("check-out/{bookingId}")]
		[SwaggerOperation(Summary = "Check out booking")]
		public async Task<IActionResult> CheckOutBooking(Guid bookingId)
		{
			var command = new CheckOutBookingCommand(bookingId);
			var result = await _mediator.Send(command);
			if (!result)
			{
				return NotFound("Booking could not be checked out.");
			}

			return Ok(new
			{
				message = "Booking checked out successfully."
			});
		}
	}
}
