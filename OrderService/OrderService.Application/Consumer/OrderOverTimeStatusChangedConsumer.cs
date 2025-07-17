using Contracts.Events;
using MassTransit;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
	public class OrderOverTimeStatusChangedConsumer : IConsumer<OrderOverTimeStatusChangedEvent>
	{
		private readonly IOrderRepositoryRead _repository;

		public OrderOverTimeStatusChangedConsumer(IOrderRepositoryRead repository)
		{
			_repository = repository;
		}

		public async Task Consume(ConsumeContext<OrderOverTimeStatusChangedEvent> context)
		{
			Console.WriteLine("OrderOverTimeStatusChangedConsumer: Consuming OrderOverTimeStatusChangedEvent");
			var evt = context.Message;
			var order = await _repository.GetOrderByIdAsync(evt.OrderId, CancellationToken.None);
			if (order == null)
			{
				throw new ArgumentException($"Order with ID {evt.OrderId} not found.");
			}
			order.Status = evt.NewStatus;
			await _repository.UpdateOrderStatus(order);
		}
	}
}
