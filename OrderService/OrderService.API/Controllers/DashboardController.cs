using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Queries;
using System.Text.RegularExpressions;

namespace OrderService.API.Controllers
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

		[HttpGet("revenue")]
		public async Task<IActionResult> GetRevenue([FromQuery] string startDate, [FromQuery] string endDate, [FromQuery] string groupBy)
		{
			switch (groupBy)
			{
				case "daily":
					var dailyQuery = new GetRevenueByDateQuery(
						startDate,
						endDate);
					var dailyResult = await _mediator.Send(dailyQuery);
					return Ok(dailyResult);
				case "monthly":
					var monthlyQuery = new GetRevenueByMonthQuery(
						startDate,
						endDate);
					var monthlyResult = await _mediator.Send(monthlyQuery);
					return Ok(monthlyResult);
				case "yearly":
					var query = new GetRevenueByYearQuery(
						startDate,
						endDate);
					var result = await _mediator.Send(query);
					return Ok(result);
				default:
					break;
			}
			return BadRequest("Invalid groupBy parameter. Valid values are 'daily', 'monthly', or 'yearly'.");
		}
	}
}
