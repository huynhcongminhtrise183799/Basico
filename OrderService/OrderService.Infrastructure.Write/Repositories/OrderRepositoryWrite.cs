using OrderService.Domain.Entities;
using OrderService.Infrastructure.Read.Repositories;
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

        public async Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default)
        {
            await _dbContext.OrderDetails.AddAsync(orderDetail, cancellationToken);
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
