using Contracts.DTOs;
using Contracts.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrdersController(IMediator mediator) { _mediator = mediator; }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
        {
            var orderId = await _mediator.Send(request);
            return Ok(new CreateOrderResponse { OrderId = orderId, Success = true });
        }
    }
}
