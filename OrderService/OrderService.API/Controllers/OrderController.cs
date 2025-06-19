using Microsoft.AspNetCore.Mvc;
using MediatR;
using OrderService.Application.Command;

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
            return CreatedAtAction(nameof(CreateOrder), new { id = orderId }, new { orderId });
        }
    }
}
