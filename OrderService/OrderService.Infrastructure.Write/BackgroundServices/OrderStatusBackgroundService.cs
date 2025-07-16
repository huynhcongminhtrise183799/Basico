using Contracts.Events;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Write.BackgroundServices
{
	public class OrderStatusBackgroundService : BackgroundService
	{
		private readonly IServiceScopeFactory _scopeFactory;

		public OrderStatusBackgroundService(IServiceScopeFactory serviceScopeFactory)
		{
			_scopeFactory = serviceScopeFactory;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await UpdateOrderStatusAsync();
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken); 
			}
		}

		private async Task UpdateOrderStatusAsync()
		{
			using (var scope = _scopeFactory.CreateScope())
			{
				var repository = scope.ServiceProvider.GetRequiredService<IOrderRepositoryWrite>();
				var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

				var orders = await repository.GetOrderOverTimeAsync();

				foreach (var order in orders)
				{
					order.Status = OrderStatus.Cancelled.ToString(); 
					await repository.UpdateOrderStatus(order);
					Console.WriteLine($"Order {order.OrderId} status updated to {order.Status} due to timeout.");
					await publishEndpoint.Publish(new OrderOverTimeStatusChangedEvent
					{
						OrderId = order.OrderId,
						NewStatus = order.Status
					});
				}
			}
		}
	}
}
