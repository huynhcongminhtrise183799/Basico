using BookingService.Application.Command;
using BookingService.Application.Event;
using BookingService.Application.Message;
using BookingService.Domain.Entities;
using BookingService.Domain.IRepository;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Handler.CommandHandler
{
	public class UpdateBookingHandler : IRequestHandler<UpdateBookingCommand, bool>
	{
		private readonly IBookingRepositoryWrite _bookingRepositoryWrite;
		private readonly IBookingSlotsRepositoryWrite _bookingSlotsRepositoryWrite;
		private readonly IPublishEndpoint _publish;

		public UpdateBookingHandler(IBookingRepositoryWrite bookingRepositoryWrite, IBookingSlotsRepositoryWrite bookingSlotsRepositoryWrite, IPublishEndpoint publish)
		{
			_bookingRepositoryWrite = bookingRepositoryWrite;
			_bookingSlotsRepositoryWrite = bookingSlotsRepositoryWrite;
			_publish = publish;
		}

		public async Task<bool> Handle(UpdateBookingCommand request, CancellationToken cancellationToken)
		{
			var booking = new Booking
			{
				BookingId = request.BookingId,
				BookingDate = request.BookingDate,
				LawyerId = request.LawyerId,
				CustomerId = request.CustomerId,
				Description = request.Description,
				Status = BookingStatus.Paid.ToString(),
				ServiceId = request.ServiceId,
				Price = request.Price
			};
			var bookingSlots = request.SlotId.Select(slotId => new BookingSlots
			{
				SlotId = Guid.Parse(slotId),
				BookingId = request.BookingId,
			}).ToList();

			var resultUpdateBooking = await _bookingRepositoryWrite.UpdateBookingAsync(booking);
			var resultUpdateBookingSlot = await _bookingSlotsRepositoryWrite.UpdateBookedSlotAsync(bookingSlots);
            if (resultUpdateBooking == false || resultUpdateBookingSlot == false)
			{
				return false;
			}
			

			var @event = new UpdateBookingEvent
			{
				BookingId = request.BookingId,
				BookingDate = request.BookingDate,
				LawyerId = request.LawyerId,
				CustomerId = request.CustomerId,
				Description = request.Description,
				Status = BookingStatus.Paid.ToString(),
				SlotId = request.SlotId,
				Price = request.Price,
				ServiceId = request.ServiceId
			};
			await _publish.Publish(@event, cancellationToken);

			return true;
		}
	}

}
