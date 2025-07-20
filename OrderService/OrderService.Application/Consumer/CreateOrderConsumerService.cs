using MassTransit;
using OrderService.Application.Event;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
    public class CreateOrderConsumerService : IConsumer<OrderCreatedEvent>
    {
        private readonly IOrderRepositoryRead _orderRepositoryRead;

        public CreateOrderConsumerService(IOrderRepositoryRead orderRepositoryRead)
        {
            _orderRepositoryRead = orderRepositoryRead;
        }

        public async Task Consume(ConsumeContext<OrderCreatedEvent> context)
        {
            var evt = context.Message;
            var order = new Order
            {
                OrderId = evt.OrderId,
                UserId = evt.UserId,
                TotalPrice = evt.TotalPrice,
				CreatedAt = DateTime.Now,
				Status = evt.Status,
                OrderDetails = evt.OrderDetails?.ConvertAll(od => new OrderDetail
                {
                    OrderDetailId = od.OrderDetailId,
                    TicketPackageId = od.TicketPackageId,
                    FormTemplateId = od.FormTemplateId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }) ?? new List<OrderDetail>()   
			};
            await _orderRepositoryRead.AddOrderAsync(order);
        }
    }
}