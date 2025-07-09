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
	public class FeedbackCreatedConsumer : IConsumer<FeedbackCreatedEvent>
	{
		private readonly IFeedbackRepositoryRead _repo;

		public FeedbackCreatedConsumer(IFeedbackRepositoryRead repo)
		{
			_repo = repo;
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
			await _repo.AddAsync(feedback);
		}
	}
}
