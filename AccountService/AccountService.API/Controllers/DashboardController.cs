using AccountService.Application.Queries.Dashboard;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class DashboardController : ControllerBase
	{
		private readonly IMediator _mediator;

		public DashboardController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet("account")]
		public async Task<IActionResult> GetAccountDashboard()
		{
			var query = new GetAccountDashboardQuery();
			var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}
