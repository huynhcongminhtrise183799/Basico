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
				BookingId = request.bookingId,
				BookingDate = request.bookingDate,
				LawyerId = request.lawyerId,
				CustomerId = request.customerId,
				Status = BookingStatus.Paid.ToString(),
			};
			var bookingSlots = request.slotId.Select(slotId => new BookingSlots
			{
				SlotId = Guid.Parse(slotId),
				BookingId = request.bookingId,
			}).ToList();

			await _bookingRepositoryWrite.UpdateBookingAsync(booking);
			await _bookingSlotsRepositoryWrite.UpdateBookedSlotAsync(bookingSlots);

			var @event = new UpdateBookingEvent
			{
				BookingId = request.bookingId,
				BookingDate = request.bookingDate,
				LawyerId = request.lawyerId,
				CustomerId = request.customerId,
				Status = BookingStatus.Paid.ToString(),
				SlotId = request.slotId,
			};
			await _publish.Publish(@event, cancellationToken);

			return true;
		}
	}

}
