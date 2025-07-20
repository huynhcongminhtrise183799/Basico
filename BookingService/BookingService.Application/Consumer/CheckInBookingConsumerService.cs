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
	public class CheckInBookingConsumerService : IConsumer<CheckInBookingEvent>
	{
		private readonly IBookingRepositoryRead _repoRead;

		public CheckInBookingConsumerService(IBookingRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<CheckInBookingEvent> context)
		{
			var bookingId = context.Message.BookingId;
			 await _repoRead.CheckInBooking(bookingId);
		}
	}
}
