using Contracts.Events;
using MassTransit;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
    public class OrderCreatedEventConsumer : IConsumer<OrderTicketPackageCreatedEvent>
    {
        private readonly IOrderRepositoryRead _dbContextWrite;

        public OrderCreatedEventConsumer(IOrderRepositoryRead dbContextWrite)
        {
            _dbContextWrite = dbContextWrite;
        }

        public async Task Consume(ConsumeContext<OrderTicketPackageCreatedEvent> context)
        {
            var evt = context.Message;

            var order = new Order
            {
                OrderId = evt.OrderId,
                UserId = evt.UserId,
                Status = OrderStatus.Pending.ToString(),
                TotalPrice = evt.Price * evt.Quantity,
				CreatedAt = DateTime.UtcNow,
				OrderDetails = new List<OrderDetail>(),

            };

            var orderDetail = new OrderDetail
            {
                OrderDetailId = Guid.NewGuid(),
                OrderId = evt.OrderId,
                TicketPackageId = evt.TicketPackageId,
                Quantity = evt.Quantity,
                Price = evt.Price,
                FormTemplateId = null,
            };

            await _dbContextWrite.AddOrderAsync(order, CancellationToken.None);
            await _dbContextWrite.AddOrderDetailAsync(orderDetail, CancellationToken.None);
            await _dbContextWrite.SaveChangesAsync(CancellationToken.None);
        }
    }
}
