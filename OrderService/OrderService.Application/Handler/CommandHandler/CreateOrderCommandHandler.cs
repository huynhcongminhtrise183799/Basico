using MassTransit;
using MediatR;
using OrderService.Application.Command;
using OrderService.Application.Event;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace OrderService.Application.Handler.CommandHandler
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
    {
        private readonly IOrderRepositoryWrite _orderRepository;
		private readonly IPublishEndpoint _publishEndpoint;
        private readonly IClientFactory _clientFactory;

		public CreateOrderCommandHandler(IOrderRepositoryWrite orderRepository, IPublishEndpoint publishEndpoint)
        {
            _orderRepository = orderRepository;
			_publishEndpoint = publishEndpoint;
		}

        public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order
            {
                UserId = request.UserId,
                TotalPrice = request.TotalPrice,
                Status = OrderStatus.Pending.ToString(),
                OrderDetails = request.OrderDetails?.Select(od => new OrderDetail
                {
                    TicketPackageId = null,
                    FormTemplateId = od.FormTemplateId,
                    Quantity = od.Quantity,
                    Price = od.Price
                }).ToList() ?? new List<OrderDetail>()
            };

            await _orderRepository.AddOrderAsync(order);

            var eventDetails = order.OrderDetails.Select(od => new OrderCreatedEvent.OrderDetailDto(
                od.OrderDetailId,
                od.TicketPackageId,
                od.FormTemplateId,
                od.Quantity,
                od.Price
            )).ToList();

            var orderCreatedEvent = new OrderCreatedEvent(
                order.OrderId,
                order.UserId,
                order.TotalPrice,
                order.Status,
                eventDetails
            );

            await _publishEndpoint.Publish(orderCreatedEvent, cancellationToken);

            return order.OrderId;
        }
    }
}