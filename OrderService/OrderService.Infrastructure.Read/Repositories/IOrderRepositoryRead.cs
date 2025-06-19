using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
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
        public OrderRepositoryRead(OrderDbContextRead dbContext)
        {
            _context = dbContext;
        }
        public async Task AddOrderAsync(Order order, CancellationToken cancellationToken = default)
        {
            await _context.Orders.AddAsync(order, cancellationToken);
        }

        public async Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default)
        {
            await _context.OrderDetails.AddAsync(orderDetail, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
