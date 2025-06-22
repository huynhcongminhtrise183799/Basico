using AccountService.Application.Queries.Shift;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
	[Route("api/")]
	[ApiController]
	public class ShiftController : ControllerBase
	{
		private readonly IMediator _mediator;
		public ShiftController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("shifts")]
		public async Task<IActionResult> GetAll()
		{
			var result = await _mediator.Send(new GetAllShiftQuery());
			if (result == null || result.Count == 0)
			{
				return NotFound(new { message = "No shifts found" });
			}
			return Ok(result);
		}
	}
}
