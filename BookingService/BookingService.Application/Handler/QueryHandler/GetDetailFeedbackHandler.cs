using BookingService.Application.DTOs.Response;
using BookingService.Application.Query;
using BookingService.Domain.IRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.Application.Handler.QueryHandler
{
	public class GetDetailFeedbackHandler : IRequestHandler<GetDetailFeedbackQuery, FeedbackResponse>
	{
		private readonly IFeedbackRepositoryRead _repo;

		public GetDetailFeedbackHandler(IFeedbackRepositoryRead repo)
		{
			_repo = repo;
		}

		public async Task<FeedbackResponse> Handle(GetDetailFeedbackQuery request, CancellationToken cancellationToken)
		{
			var feedback = await _repo.GetByFeedbackIdAsync(request.FeedbackId);
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
