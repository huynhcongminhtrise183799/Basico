using MassTransit;
using OrderService.Application.Event;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Consumer
{
	public class CreatePaymentConsumerService : IConsumer<CreatePaymentEvent>
	{
		private readonly IPaymentRepositoryRead _repoRead;
		public CreatePaymentConsumerService(IPaymentRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}
		public async Task Consume(ConsumeContext<CreatePaymentEvent> context)
		{
			var paymentEvent = context.Message;
			var payment = new Payment
			{
				PaymentId = paymentEvent.PaymentId,
				OrderId = paymentEvent.OrderId,
				BookingId = paymentEvent.BookingId,
				Amount = paymentEvent.Amount,
				PaymentDate = paymentEvent.PaymentDate,
				Status = paymentEvent.Status,
				PaymentMethod = paymentEvent.Method
			};
			await _repoRead.AddPaymentAsync(payment);
			Console.WriteLine("Save Postgre successfully");
		}
	}
}
