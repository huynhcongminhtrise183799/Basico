using BookingService.Application.Command;
using BookingService.Application.DTOs.Response;
using BookingService.Application.Event;
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
	public class UpdateFeedbackHandler : IRequestHandler<UpdateFeedbackCommand, bool>
	{
		private readonly IFeedbackRepositoryWrite _repo;
		private readonly IPublishEndpoint _publish;

		public UpdateFeedbackHandler(IFeedbackRepositoryWrite repo, IPublishEndpoint publish)
		{
			_repo = repo;
			_publish = publish;
		}

		public async Task<bool> Handle(UpdateFeedbackCommand request, CancellationToken cancellationToken)
		{
			var feedback = new Feedback
			{
				FeedbackId = request.FeedbackId,
				FeedbackContent = request.FeedbackContent,
				Rating = request.Rating
			};
			await _repo.UpdateAsync(feedback);
			var @event = new FeedbackUpdatedEvent
			{
				FeedbackId = request.FeedbackId,
				FeedbackContent = request.FeedbackContent,
				Rating = request.Rating
			};
			await _publish.Publish(@event);
			return true;
		}
	}
}
