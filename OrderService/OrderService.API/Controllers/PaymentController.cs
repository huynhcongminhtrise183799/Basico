using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command;
using OrderService.Application.DTOs.Vnpay;
using OrderService.Application.IService;

namespace OrderService.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PaymentController : Controller
	{
		private readonly IPaymentService _paymentService;
		private readonly IMediator _mediator;

		public PaymentController(IPaymentService paymentService, IMediator mediator)
		{
			_paymentService = paymentService;
			_mediator = mediator;
		}

		[HttpPost("create-payment-url")]
		public IActionResult CreatePaymentUrl([FromBody] PaymentInformationModel model)
		{
			var paymentUrl = _paymentService.CreatePaymentUrl(model, HttpContext);
			return Ok(new { Url = paymentUrl });
		}

		[HttpGet("payment-callback")]
		public async Task<IActionResult> PaymentCallback()
		{
			var response = _paymentService.PaymentExecute(Request.Query);

			if (response.Success)
			{
				await _mediator.Send(new CreatePaymentCommand(response));
				return Ok(response); // response là PaymentResponseModel
			}
			else
			{
				return BadRequest(new
				{
					message = "Payment fail"
				}); // response là PaymentResponseModel
			}


		}

	}
}