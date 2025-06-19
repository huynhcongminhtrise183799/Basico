using Contracts;
using MassTransit;
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
		private readonly IPaymentRepositoryRead _repoRead;

		public CreatePaymentCommandHandler(IPaymentRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint, IPaymentRepositoryRead repoRead)
		{
			_publishEndpoint = publishEndpoint;
			_repoRead = repoRead;
			_repoWrite = repoWrite;
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
			return payment.PaymentId.ToString();
		}
	}
}
