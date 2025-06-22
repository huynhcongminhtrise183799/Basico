using BookingService.Application.Command;
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
	public class CheckOutBookingConsumer : IConsumer<CheckOutBookingEvent>
	{
		private readonly IBookingRepositoryRead _repoRead;

		public CheckOutBookingConsumer(IBookingRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<CheckOutBookingEvent> context)
		{
			var command = context.Message;
			await _repoRead.UpdateStatusBookingToCompleted(command.BookingId);
		}
	}
}
