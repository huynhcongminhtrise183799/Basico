using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
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

        [HttpPost("payment-callback")]
        public async Task<IActionResult> PaymentCallbackPost([FromBody] Dictionary<string, string> formData)
        {
            var collections = new QueryCollection(formData.ToDictionary(kv => kv.Key, kv => new StringValues(kv.Value)));

            var response = _paymentService.PaymentExecute(collections);

            if (response.Success)
            {
                await _mediator.Send(new CreatePaymentCommand(response));
                return Ok(response);
            }
            else
            {
                return BadRequest(new { message = "Payment fail" });
            }
        }

    }
}
