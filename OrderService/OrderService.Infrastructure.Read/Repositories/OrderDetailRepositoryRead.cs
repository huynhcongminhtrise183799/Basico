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
	public class OrderDetailRepositoryRead : IOrderDetailRepositoryRead
	{
		private readonly OrderDbContextRead _context;
		public OrderDetailRepositoryRead(OrderDbContextRead context)
		{
			_context = context ;
		}
		public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAndCustomerId(Guid orderId, Guid customerId)
		{
			return await _context.OrderDetails.Include(od => od.Order)
				.Where(od => od.OrderId == orderId && od.Order.UserId == customerId)
				.ToListAsync();
		}
	}
}
