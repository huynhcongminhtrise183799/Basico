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
	public class GetAllFeedbackHandler : IRequestHandler<GetAllFeedbackQuery, List<FeedbackResponse>>
	{
		private readonly IFeedbackRepositoryRead _repo;

		public GetAllFeedbackHandler(IFeedbackRepositoryRead repo)
		{
			_repo = repo;
		}

		public async Task<List<FeedbackResponse>> Handle(GetAllFeedbackQuery request, CancellationToken cancellationToken)
		{
			var feedbacks = await _repo.GetAllAsync();
			var result = feedbacks.Select(f => new FeedbackResponse
			{
				BookingId = f.BookingId,
				CustomerId = f.CustomerId,
				FeedbackContent = f.FeedbackContent,
				FeedbackDay = f.FeedbackDay,
				FeedbackId = f.FeedbackId,
				Rating = f.Rating,
			}).ToList();
			return result;
		}
	}
}
