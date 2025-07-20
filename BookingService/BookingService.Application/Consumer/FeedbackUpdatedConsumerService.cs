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
	public class FeedbackUpdatedConsumerService : IConsumer<FeedbackUpdatedEvent>
	{
		private readonly IFeedbackRepositoryRead _repository;

		public FeedbackUpdatedConsumerService(IFeedbackRepositoryRead repository)
		{
            _repository = repository;
		}

		public async Task Consume(ConsumeContext<FeedbackUpdatedEvent> context)
		{
			var message = context.Message;
			var feedback = new Feedback
			{
				FeedbackId = message.FeedbackId,
				FeedbackContent = message.FeedbackContent,
				Rating = message.Rating
			};
			await _repository.UpdateAsync(feedback);
		}
	}
}
