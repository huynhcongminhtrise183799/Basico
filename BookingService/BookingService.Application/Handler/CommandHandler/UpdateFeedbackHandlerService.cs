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
	public class UpdateFeedbackHandlerService : IRequestHandler<UpdateFeedbackCommand, bool>
	{
		private readonly IFeedbackRepositoryWrite _repository;
		private readonly IPublishEndpoint _publish;

		public UpdateFeedbackHandlerService(IFeedbackRepositoryWrite repository, IPublishEndpoint publish)
		{
            _repository = repository;
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
			var result =  await _repository.UpdateAsync(feedback);
			if (result)
			{
                var @event = new FeedbackUpdatedEvent
                {
                    FeedbackId = request.FeedbackId,
                    FeedbackContent = request.FeedbackContent,
                    Rating = request.Rating
                };
                await _publish.Publish(@event);
                return true;
            }
			return false;
		}
	}
}
