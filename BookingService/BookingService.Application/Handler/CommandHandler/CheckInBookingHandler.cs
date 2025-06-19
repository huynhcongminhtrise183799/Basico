using BookingService.Application.Command;
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
	public class CheckInBookingHandler : IRequestHandler<CheckInBookingCommand, bool>
	{
		private readonly IBookingRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publish;

		public CheckInBookingHandler(IBookingRepositoryWrite repoWrite, IPublishEndpoint publish)
		{
			_repoWrite = repoWrite;
			_publish = publish;
		}

		public async Task<bool> Handle(CheckInBookingCommand request, CancellationToken cancellationToken)
		{
			await _repoWrite.CheckInBookingAsync(request.bookingId);
			var @event = new
			{
				BookingId = request.bookingId,
			};
			await _publish.Publish(@event, cancellationToken);
			return true;
		}
	}
}
