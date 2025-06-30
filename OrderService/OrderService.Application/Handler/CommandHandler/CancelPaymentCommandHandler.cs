using MassTransit;
using MediatR;
using OrderService.Application.Command;
using OrderService.Application.Event;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Handler.CommandHandler
{
	public class CancelPaymentCommandHandler : IRequestHandler<CancelPaymentCommand, bool>
	{
		private readonly IOrderRepositoryWrite _orderRepository;
		private readonly IPublishEndpoint _publishEndpoint;
		public CancelPaymentCommandHandler(IOrderRepositoryWrite orderRepository, IPublishEndpoint publishEndpoint)
		{
			_orderRepository = orderRepository ;
			_publishEndpoint = publishEndpoint ;
		}
		public async Task<bool> Handle(CancelPaymentCommand request, CancellationToken cancellationToken)
		{
			await _orderRepository.CancelOrderAsync(request.OrderId);
			var @event = new OrderCancelledEvent
			{
				OrderId = request.OrderId
			};
			await _publishEndpoint.Publish(@event, cancellationToken);
			return true;
		}
	}
}
