using BookingService.Application.Command;
using BookingService.Application.Event;
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
	public class CancelBookingHandlerService : IRequestHandler<CancelBookingCommand, bool>
	{
		private readonly IBookingRepositoryWrite _bookingRepositoryWrite;
		private readonly IPublishEndpoint _publish;
		public CancelBookingHandlerService(IBookingRepositoryWrite bookingRepositoryWrite, IPublishEndpoint publishEndpoint)
		{
			_bookingRepositoryWrite = bookingRepositoryWrite;
			_publish = publishEndpoint;
		}

		public async Task<bool> Handle(CancelBookingCommand request, CancellationToken cancellationToken)
		{
			// Delete the booking
			var result = await _bookingRepositoryWrite.DeleteBookingAsync(request.bookingId);
			if (result)
			{
                await _publish.Publish(new CancelBookingEvent
                {
                    BookingId = request.bookingId
                }, cancellationToken);
                // Return true to indicate success
                return true;
            }
			return false;
		}
	}
}
