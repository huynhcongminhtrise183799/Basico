using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command;
using Contracts.DTOs;
using OrderService.Application.Queries;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("create-form")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderId = await _mediator.Send(request);
            return Ok(orderId);
        }

        [HttpPost("ticket-package")]
        public async Task<IActionResult> CreateOrderTicketPackage([FromBody] CreateOrderTicketPackageCommand request)
        {
            var orderId = await _mediator.Send(request);
            return Ok(new CreateOrderResponse { OrderId = orderId, Success = true });
        }

        [HttpGet]
		public async Task<IActionResult> GetOrders([FromQuery] Guid orderId, [FromQuery] string status)
		{
			var query = new GetOderByOderIdAndStatusQuery(orderId, status);
            var result = await _mediator.Send(query);
			return Ok(result);
		}
	}
}
