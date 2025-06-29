using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.IRepositories
{
	public interface IOrderRepositoryRead
	{
        Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
		Task<Order> GetOrderByIdAsync(Guid? orderId, CancellationToken cancellationToken = default);

		Task UpdateOrderStatus(Order order);

		Task<Order?> GetOrderByIdAndStatusAsync(Guid orderId, string status);
	}
}
