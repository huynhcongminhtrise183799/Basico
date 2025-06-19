using MassTransit;
using OrderService.Application.Event;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
	public class UpdateOrderStatusConsumer : IConsumer<UpdateOrderStatusEvent>
	{
		private readonly IOrderRepositoryRead _repoRead;
		private readonly IOrderRepositoryWrite _repoWrite;
		public UpdateOrderStatusConsumer(IOrderRepositoryRead repoRead, IOrderRepositoryWrite repoWrite)
		{
			_repoRead = repoRead;
			_repoWrite = repoWrite;
		}
		public async Task Consume(ConsumeContext<UpdateOrderStatusEvent> context)
		{
			var evt = context.Message;
			var order = await _repoRead.GetOrderByIdAsync(evt.OrderId, CancellationToken.None);
			order.Status = OrderStatus.Completed.ToString();
			await _repoWrite.UpdateOrderStatus(order);
			await _repoRead.UpdateOrderStatus(order);

		}
	}
}
