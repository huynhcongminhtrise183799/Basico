using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Command;
using Contracts.DTOs;
using OrderService.Application.Queries;

namespace OrderService.API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class OrderController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [Route("order/create-form")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var orderId = await _mediator.Send(request);
            return Ok(orderId);
        }

        [HttpPost("order/ticket-package")]
        public async Task<IActionResult> CreateOrderTicketPackage([FromBody] CreateOrderTicketPackageCommand request)
        {
            var orderId = await _mediator.Send(request);
            return Ok(new CreateOrderResponse { OrderId = orderId, Success = true });
        }

        [HttpGet("order")]
        public async Task<IActionResult> GetOrders([FromQuery] Guid orderId, [FromQuery] string status)
        {
            var query = new GetOderByOderIdAndStatusQuery(orderId, status);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("orders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _mediator.Send(new GetAllOrderQuery());
            return Ok(result);
        }

        [HttpDelete("order/{id}")]
        public async Task<IActionResult> CancelOrder(Guid id)
        {
            var command = new CancelPaymentCommand(id);
            var result = await _mediator.Send(command);
            if (result)
                return Ok(new { Success = true, Message = "Order cancelled successfully." });
            else
                return BadRequest(new { Success = false, Message = "Failed to cancel order." });
        }
    }
}
