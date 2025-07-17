using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Write.Repositories
{
	public class OrderRepositoryWrite : IOrderRepositoryWrite
	{
        private readonly OrderDbContextWrite _dbContext;
        public OrderRepositoryWrite(OrderDbContextWrite dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _dbContext.Orders.AddAsync(order, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default)
		{
            await _dbContext.OrderDetails.AddAsync(orderDetail, cancellationToken);
		}

		public async Task CancelOrderAsync(Guid orderId)
		{
			var order = _dbContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
			if (order != null)
			{
				order.Status = OrderStatus.Cancelled.ToString();
				await _dbContext.SaveChangesAsync();
			}
			
		}

		public async Task<List<Order>> GetOrderOverTimeAsync()
		{
			var now = DateTime.Now;

			return await _dbContext.Orders
				.Where(o =>
					o.Status.ToLower() == OrderStatus.Pending.ToString().ToLower() &&
					o.CreatedAt.AddMinutes(15) <= now)
				.ToListAsync();
		}



		public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
		{
            await _dbContext.SaveChangesAsync(cancellationToken);
		}

		public async Task UpdateOrderStatus(Order order)
		{
            var existingOrder = _dbContext.Orders.FirstOrDefault(o => o.OrderId == order.OrderId);
			if (existingOrder != null)
			{
				existingOrder.Status = order.Status;
				await _dbContext.SaveChangesAsync();
			}

		}
	}
}
