using BookingService.Application.Event;
using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Consumer
{
	public class UpdateBookingConsumerService : IConsumer<UpdateBookingEvent>
	{
		private readonly IBookingRepositoryRead _bookingRepositoryRead;
		private readonly IBookingSlotRepositoryRead _bookingSlotRepositoryRead;

		public UpdateBookingConsumerService(IBookingRepositoryRead bookingRepositoryRead, IBookingSlotRepositoryRead bookingSlotRepositoryRead)
		{
			_bookingRepositoryRead = bookingRepositoryRead;
			_bookingSlotRepositoryRead = bookingSlotRepositoryRead;
		}

		public async Task Consume(ConsumeContext<UpdateBookingEvent> context)
		{
			var message = context.Message;
			var booking = new Booking
			{
				BookingId = message.BookingId,
				CustomerId = message.CustomerId,
				LawyerId = message.LawyerId,
				BookingDate = message.BookingDate,
				Status = message.Status,
				ServiceId = message.ServiceId,
				Price = message.Price,
				Description = message.Description,
			};
			var bookingSlots = message.SlotId.Select(slotId => new BookingSlots
			{
				SlotId = Guid.Parse(slotId),
				BookingId = message.BookingId,
			}).ToList();
			await _bookingRepositoryRead.UpdateBookingAsync(booking);
			await _bookingSlotRepositoryRead.UpdateBookedSlotAsync(bookingSlots);
		}
	}
}
