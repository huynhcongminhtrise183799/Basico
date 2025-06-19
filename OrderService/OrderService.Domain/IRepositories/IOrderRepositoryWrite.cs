using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Read.Repositories
{
    public interface IOrderRepositoryWrite
    {
        Task AddOrderAsync(Order order, CancellationToken cancellationToken = default);
        Task AddOrderDetailAsync(OrderDetail orderDetail, CancellationToken cancellationToken = default);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
