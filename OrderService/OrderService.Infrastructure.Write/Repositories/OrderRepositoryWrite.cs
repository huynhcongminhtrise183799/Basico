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
		private readonly OrderDbContextWrite _context;
		public OrderRepositoryWrite(OrderDbContextWrite context)
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
