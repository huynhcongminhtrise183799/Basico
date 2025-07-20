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
	public class FeedbackCreatedConsumerService : IConsumer<FeedbackCreatedEvent>
	{
		private readonly IFeedbackRepositoryRead _repository;

		public FeedbackCreatedConsumerService(IFeedbackRepositoryRead repository)
		{
            _repository = repository;
		}

		public async Task Consume(ConsumeContext<FeedbackCreatedEvent> context)
		{
			var message = context.Message;
			var feedback = new Feedback
			{
				FeedbackId = message.FeedbackId,
				BookingId = message.BookingId,
				CustomerId = message.CustomerId,
				FeedbackContent = message.FeedbackContent,
				FeedbackDay = message.FeedbackDay,
				Rating = message.Rating
			};
			await _repository.AddAsync(feedback);
		}
	}
}
