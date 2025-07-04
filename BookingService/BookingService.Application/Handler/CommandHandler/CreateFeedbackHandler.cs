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
	public class CreateFeedbackHandler : IRequestHandler<CreateFeedbackCommand, FeedbackResponse>
	{
		private readonly IFeedbackRepositoryWrite _repo;
		private readonly IPublishEndpoint _publish;

		public CreateFeedbackHandler(IFeedbackRepositoryWrite repo, IPublishEndpoint publish)
		{
			_repo = repo;
			_publish = publish;
		}

		public async Task<FeedbackResponse> Handle(CreateFeedbackCommand request, CancellationToken cancellationToken)
		{
			var feedback = new Feedback
			{
				FeedbackId = Guid.NewGuid(),
				BookingId = request.BookingId,
				CustomerId = request.CustomerId,
				FeedbackContent = request.FeedbackContent,
				FeedbackDay = DateOnly.FromDateTime(DateTime.Now),
				Rating = request.Rating
			};

			await _repo.AddAsync(feedback);
			var @event = new FeedbackCreatedEvent
			{
				FeedbackId = feedback.FeedbackId,
				BookingId = feedback.BookingId,
				CustomerId = feedback.CustomerId,
				FeedbackContent = feedback.FeedbackContent,
				FeedbackDay = feedback.FeedbackDay,
				Rating = feedback.Rating
			};
			await _publish.Publish(@event);

			return new FeedbackResponse
			{
				FeedbackId = feedback.FeedbackId,
				BookingId = feedback.BookingId,
				CustomerId = feedback.CustomerId,
				FeedbackContent = feedback.FeedbackContent,
				FeedbackDay = feedback.FeedbackDay,
				Rating = feedback.Rating
			};
		}
	}
}
