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
	public class CreateBookingConsumer : IConsumer<CreateBookingEvent>
	{
		private readonly IBookingRepositoryRead _repository;

		public CreateBookingConsumer(IBookingRepositoryRead repository)
		{
            _repository = repository;
		}

		public async Task Consume(ConsumeContext<CreateBookingEvent> context)
		{
			var bookingEvent = context.Message;
			var booking = new Booking
			{
				BookingId = bookingEvent.BookingId,
				CustomerId = bookingEvent.CustomerId,
				LawyerId = bookingEvent.LawyerId,
				ServiceId = bookingEvent.ServiceId,
				BookingDate = bookingEvent.BookingDate,
				Price = bookingEvent.Price,
				Status = bookingEvent.Status,
				Description = bookingEvent.Description,
				CreatedAt = DateTime.UtcNow,
				BookingSlots = bookingEvent.SlotId.Select(slotId => new BookingSlots
                {
                    SlotId = Guid.Parse(slotId),
                    BookingId = bookingEvent.BookingId
                }).ToList()
            };
			//var bookingSlots = bookingEvent.SlotId.Select(slotId => new BookingSlots
			//{
			//	SlotId = Guid.Parse(slotId),
			//	BookingId = booking.BookingId
			//}).ToList();
			await _repository.CreateBookingAsync(booking);
			//await _bookingSlotRepositoryRead.AddBookedSlotAsync(bookingSlots);
		}
	}
}
