using BookingService.Domain.IRepository;
using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Consumer
{
	public class BookingOverTimeStatusChangedConsumerService : IConsumer<BookingOverTimeStatusChangedEvent>
	{
		private readonly IBookingRepositoryRead _repository;

		public BookingOverTimeStatusChangedConsumerService(IBookingRepositoryRead repository)
		{
			_repository = repository;
		}

		public async Task Consume(ConsumeContext<BookingOverTimeStatusChangedEvent> context)
		{
			Console.WriteLine("BookingOverTimeStatusChangedConsumer: Consuming BookingOverTimeStatusChangedEvent");
			var bookingEvent = context.Message;
			var booking = await _repository.GetBookingByIdAsync(bookingEvent.BookingId);
			booking.Status = bookingEvent.Status;
			await _repository.UpdateBookingAsync(booking);
		}
	}

}
