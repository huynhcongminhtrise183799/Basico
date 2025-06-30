using MassTransit;
using OrderService.Application.Event;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
	public class OrderCancelledConsumer : IConsumer<OrderCancelledEvent>
	{
		private readonly IOrderRepositoryRead _repo;
		public OrderCancelledConsumer(IOrderRepositoryRead repo)
		{
			_repo = repo;
		}
		public async Task Consume(ConsumeContext<OrderCancelledEvent> context)
		{
			var orderId = context.Message.OrderId;
			// Logic to handle the order cancellation, e.g., updating the order status in the database
			await _repo.CancelOrderAsync(orderId);
		}

	}
}
