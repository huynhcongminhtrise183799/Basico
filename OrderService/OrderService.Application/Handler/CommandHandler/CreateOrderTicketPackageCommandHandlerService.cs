﻿using Contracts.Events;
using MassTransit;
using MediatR;
using OrderService.Application.Command;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Handler.CommandHandler
{
    class CreateOrderTicketPackageCommandHandlerService : IRequestHandler<CreateOrderTicketPackageCommand, Guid>
    {
        private readonly IOrderRepositoryWrite _orderRepository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IClientFactory _clientFactory;

        public CreateOrderTicketPackageCommandHandlerService(IOrderRepositoryWrite orderRepository, IPublishEndpoint publishEndpoint, IClientFactory clientFactory)
        {
            _orderRepository = orderRepository;
            _publishEndpoint = publishEndpoint;
            _clientFactory = clientFactory;
        }

        public async Task<Guid> Handle(CreateOrderTicketPackageCommand request, CancellationToken cancellationToken)
        {
            //var getRequest = new GetRequestAmountEvent
            //{
            //    TicketPackageId = request.TicketPackageId
            //};
            //var client = _clientFactory.CreateRequestClient<GetRequestAmountEvent>();
            //var response = await client.GetResponse<RequestAmountResponseEvent>(getRequest, cancellationToken);
            var orderId  = Guid.NewGuid();

			var order = new Order
            {
                OrderId = orderId,
                UserId = request.UserId,
                OrderDetails = new List<OrderDetail>(),
                TotalPrice = request.Price * request.Quantity,
				CreatedAt = DateTime.Now,
				Status = OrderStatus.Pending.ToString()
            };
            await _orderRepository.AddOrderAsync(order, cancellationToken);

            var orderDetail = new OrderDetail
            {
                OrderDetailId = Guid.NewGuid(),
                OrderId = orderId,
                TicketPackageId = request.TicketPackageId,
                Quantity = request.Quantity,
                Price = request.Price
            };
            await _orderRepository.AddOrderDetailAsync(orderDetail, cancellationToken);

            await _orderRepository.SaveChangesAsync(cancellationToken);

            var evt = new OrderTicketPackageCreatedEvent
            {
                OrderId = orderId,
                UserId = request.UserId,
                TicketPackageId = request.TicketPackageId,
                Quantity = request.Quantity,
                Price = request.Price
            };
            await _publishEndpoint.Publish(evt, cancellationToken);

            //var ticketPackageUpdate = new UpdateAccountTicketRequestEvent
            //{
            //    CustomerID = request.UserId,
            //    TicketRequestAmount = request.Quantity * response.Message.RequestAmount,
            //};
            //await _publishEndpoint.Publish(ticketPackageUpdate, cancellationToken);

            return order.OrderId;
        }
    }
}
