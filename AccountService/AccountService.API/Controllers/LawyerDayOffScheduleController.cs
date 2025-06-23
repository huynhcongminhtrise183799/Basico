using AccountService.Application.Commands.DayOff;
using AccountService.Application.DTOs.Request;
using AccountService.Application.Handler.QueryHandler.DayOff;
using AccountService.Application.Queries.DayOff;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace AccountService.API.Controllers
{
	[Route("api/")]
	[ApiController]
	public class LawyerDayOffScheduleController : ControllerBase
	{
		private readonly IMediator _mediator;

		public LawyerDayOffScheduleController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("day-off")]
		[SwaggerOperation(Summary = "Đăng ký ngày nghỉ(NOTE: trc khi chạy api thì gọi api get hết shift lên nha !!!!)")]
		public async Task<IActionResult> CreateDayOffSchedule([FromBody] RegisterDayOffCommand command)
		{
			var result = await _mediator.Send(command);
			if (result != null)
			{
				return Ok(new { message = "Day off schedule created successfully" });
			}
			return BadRequest(new { message = "Failed to create day off schedule" });
		}

		[HttpPut("day-off/{id}")]
		[SwaggerOperation(Summary = "Lawyer tự update lịch nghỉ(ngày mới hoặc nghỉ ca khác )")]
		public async Task<IActionResult> UpdateDayOffSchedule(Guid id, [FromBody] UpdateDayOffRequest request)
		{
			var command = new UpdateDayOffCommand(id, request.DayOff,request.ShiftId);
			var result = await _mediator.Send(command);
			if (result)
			{
				return Ok(new { message = "Day off schedule updated successfully" });
			}
			return BadRequest(new { message = "Failed to update day off schedule" });
		}

		[HttpDelete("day-off/{id}")]
		[SwaggerOperation(Summary = "Lawyer xóa lịch nghỉ ")]
		public async Task<IActionResult> DeleteDayOffSchedule(Guid id)
		{
			var command = new DeleteDayOffCommand(id);
			var result = await _mediator.Send(command);
			if (result)
			{
				return Ok(new { message = "Day off schedule deleted successfully" });
			}
			return BadRequest(new { message = "Failed to delete day off schedule" });
		}

		[HttpPut("day-off/justify/{id}")]
		[SwaggerOperation(Summary = "Admin chấp thuận hoặc ko chấp thuận lịch nghỉ. 1.APPROVED, 2. REJECTED")]
		public async Task<IActionResult> JustifyDayOff(Guid id, [FromBody] List<JustifyDayOffRequest> requests)
		{
			var command = new JustifyDayOffCommand(id, requests);
			var result = await _mediator.Send(command);
			if (result)
			{
				return Ok(new { message = "Day off justified successfully" });
			}
			return BadRequest(new { message = "Failed to justify day off" });
		}
		[HttpGet("day-off")]
		[SwaggerOperation(Summary = "Lấy ra các lịch nghỉ từ ngày bao nhiêu đến ngày bao nhiêu(Dành cho Admin)")]
		public async Task<IActionResult> GetDayOffScheduleByStatus([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate)
		{
			var query = new GetDayOffsBetween(fromDate, toDate);
			var result = await _mediator.Send(query);
			if (result != null)
			{
				return Ok(result);
			}
			return NotFound(new { message = "No day off schedule found for the specified lawyer" });
		}
		[HttpGet("day-off/{id}")]
		[SwaggerOperation(Summary = "Xem detail 1 lịch nghỉ ")]
		public async Task<IActionResult> GetDayOffScheduleById(Guid id)
		{
			var query = new GetDetailDayOffQuery(id);
			var result = await _mediator.Send(query);
			if (result != null)
			{
				return Ok(result);
			}
			return NotFound(new { message = "No day off schedule found for the specified ID" });
		}
	}
}
