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
	public class CheckOutBookingHandlerService : IRequestHandler<CheckOutBookingCommand, bool>
	{
		private readonly IBookingRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;
		public CheckOutBookingHandlerService(IBookingRepositoryWrite repoWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_publish = publish;
		}
		public async Task<bool> Handle(CheckOutBookingCommand request, CancellationToken cancellationToken)
		{
			var result = await _repoWrite.UpdateStatusBookingToCompleted(request.bookingId);
			if (!result)
			{
				return false;
			}
			var @event = new CheckOutBookingEvent
			{
				BookingId = request.bookingId
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}

}
