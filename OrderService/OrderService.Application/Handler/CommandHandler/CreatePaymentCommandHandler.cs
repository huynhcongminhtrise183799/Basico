using Contracts;
using Contracts.Events;
using MassTransit;
using MassTransit.Clients;
using MediatR;
using OrderService.Application.Command;
using OrderService.Application.Event;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace OrderService.Application.Handler.CommandHandler
{
	public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
	{
		private readonly IPaymentRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publishEndpoint;
		private readonly IOrderRepositoryRead _orderRepositoryRead;
		private readonly IClientFactory _clientFactory;

		public CreatePaymentCommandHandler(IPaymentRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint, IOrderRepositoryRead orderRepositoryRead1, IClientFactory clientFactory)
		{
			_publishEndpoint = publishEndpoint;
			_repoWrite = repoWrite;
			_orderRepositoryRead = orderRepositoryRead1;
			_clientFactory = clientFactory;
		}

		public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
		{
			var model = request.Response;
			var payment = new Payment
			{
				PaymentId = Guid.NewGuid(),
				OrderId = model.IsBooking ? null : model.TargetId,
				BookingId = model.IsBooking ? model.TargetId : null,
				Amount = model.Amount,
				PaymentDate = DateTime.Now,
				Status = model.Status.ToString(),
				PaymentMethod = model.PaymentMethod.ToString()
			};
			

			await _repoWrite.AddPaymentAsync(payment);

			var timePostgre = DateTime.UtcNow;
			var createPaymentEvent = new CreatePaymentEvent
			(
				payment.PaymentId,
				payment.OrderId,
				payment.BookingId,
				payment.Amount,
				timePostgre,
				payment.Status,
				payment.PaymentMethod
			);
			
			await _publishEndpoint.Publish(createPaymentEvent, cancellationToken);
			if (model.IsBooking)
			{
				var @event = new PaymentSuccessEvent
				{
					BookingId = payment.BookingId
				};
				await _publishEndpoint.Publish(@event, cancellationToken);
			}
			else
			{
				var order = await _orderRepositoryRead.GetOrderByIdAsync(payment.OrderId, cancellationToken);
				var getRequest = new GetRequestAmountEvent
				{
					TicketPackageId = order.OrderDetails.Select(od => od.TicketPackageId).FirstOrDefault() ?? Guid.Empty
				};
				var client = _clientFactory.CreateRequestClient<GetRequestAmountEvent>();
				var response = await client.GetResponse<RequestAmountResponseEvent>(getRequest, cancellationToken);
				var ticketPackageUpdate = new UpdateAccountTicketRequestEvent
				{
					CustomerID = order.UserId,
					TicketRequestAmount  = order.OrderDetails.Sum(od => od.Quantity * response.Message.RequestAmount)
				};
				var @event = new UpdateOrderStatusEvent
				{
					OrderId = payment.OrderId
				};
				await _publishEndpoint.Publish(@event, cancellationToken);
				await _publishEndpoint.Publish(ticketPackageUpdate, cancellationToken);

			}
			return payment.PaymentId.ToString();
		}
	}
}
