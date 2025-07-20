using BookingService.Domain.IRepository;
using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Consumer
{
	public class PaymentSuccessConsumerService : IConsumer<PaymentSuccessEvent>
	{
		private readonly IBookingRepositoryRead _repoRead;
		private readonly IBookingRepositoryWrite _repoWrite;

		public PaymentSuccessConsumerService(IBookingRepositoryRead repoRead, IBookingRepositoryWrite repoWrite)
		{
			_repoRead = repoRead;
			_repoWrite = repoWrite;
		}

		public async Task Consume(ConsumeContext<PaymentSuccessEvent> context)
		{
			await _repoWrite.UpdateStatusBookingToPaid(context.Message.BookingId);
			await _repoRead.UpdateStatusBookingToPaid(context.Message.BookingId);
		}
	}
}
