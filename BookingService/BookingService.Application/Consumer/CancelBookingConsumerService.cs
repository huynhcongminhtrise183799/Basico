using BookingService.Application.Event;
using BookingService.Domain.IRepository;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Consumer
{
	public class CancelBookingConsumerService : IConsumer<CancelBookingEvent>
	{
		private readonly IBookingRepositoryRead _bookingRead;

		public CancelBookingConsumerService(IBookingRepositoryRead bookingRead)
		{
			_bookingRead = bookingRead;
		}

		public async Task Consume(ConsumeContext<CancelBookingEvent> context)
		{
			var message = context.Message;
			await _bookingRead.DeleteBookingAsync(message.BookingId);
		}
	}
}
