using FormService.Application.Command;
using FormService.Application.DTOs.Request;
using FormService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormService.API.Controllers
{
	[Route("api/")]
	[ApiController]
	public class CustomerFormController : ControllerBase
	{
		private readonly IMediator _mediator;
		public CustomerFormController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet("form/customer/{customerId}")]
		public async Task<IActionResult> GetCustomerFormByCustomerId(Guid customerId)
		{
			if (customerId == Guid.Empty)
			{
				return BadRequest(new
				{
					message = "Invalid customer ID."
				});
			}

			var query = new GetCustomerFormByCustomerIdQuery(customerId);
			var result = await _mediator.Send(query);

			if (result == null || !result.Any())
			{
				return NotFound(new
				{
					message = "No forms found for the specified customer."
				});
			}

			return Ok(result);
		}
		[HttpPut("customer-form/{customerFormId}")]
		public async Task<IActionResult> UpdateCustomerForm(Guid customerFormId, [FromBody] UpdateCustomerFormRequest request)
		{

			var command = new UpdateCustomerFormCommand(customerFormId, request.FormData);
			try
			{
			 var result = 	await _mediator.Send(command);
				return Ok(result);
			}
			catch (KeyNotFoundException ex)
			{
				return NotFound(new
				{
					message = ex.Message
				});
			}
			catch (Exception ex)
			{
				return StatusCode(500, new
				{
					message = "An error occurred while updating the form.",
					details = ex.Message
				});
			}
		}
		[HttpGet("customer-form/{customerFormId}")]
		public async Task<IActionResult> GetCustomerFormById(Guid customerFormId)
		{
			if (customerFormId == Guid.Empty)
			{
				return BadRequest(new
				{
					message = "Invalid customer form ID."
				});
			}

			var query = new GetCustomerFormByIdQuery(customerFormId);
			var result = await _mediator.Send(query);

			if (result == null)
			{
				return NotFound(new
				{
					message = "Customer form not found."
				});
			}

			return Ok(result);
		}
	}
}
