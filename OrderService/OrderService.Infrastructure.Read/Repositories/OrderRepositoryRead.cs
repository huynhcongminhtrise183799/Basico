using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Read.Repositories
{
	public class OrderRepositoryRead : IOrderRepositoryRead
	{
		private readonly OrderDbContextRead _context;
		public OrderRepositoryRead(OrderDbContextRead context)
		{
			_context = context;
		}
		public async Task AddOrderAsync(Order order)
		{
			await _context.Orders.AddAsync(order);
			await _context.SaveChangesAsync();
		}

        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
			await _context.SaveChangesAsync(cancellationToken);
		}

        public async Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default)
        {
            await _context.OrderDetails.AddAsync(orderDetail, cancellationToken);
        }

		public async Task<Order?> GetOrderByIdAndStatusAsync(Guid orderId, string status)
		{
			return await _context.Orders
				.FirstOrDefaultAsync(o => o.OrderId == orderId && o.Status.ToUpper() == status.ToUpper());
		}

		public async Task<Order> GetOrderByIdAsync(Guid? orderId, CancellationToken cancellationToken = default)
		{
			return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

		public async Task UpdateOrderStatus(Order order)
		{
			var existOrder = await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == order.OrderId);
			if (existOrder != null)
			{
				existOrder.Status = order.Status;
				await _context.SaveChangesAsync();
			}
		}
	}
}
