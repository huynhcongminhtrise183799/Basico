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
	}
}
