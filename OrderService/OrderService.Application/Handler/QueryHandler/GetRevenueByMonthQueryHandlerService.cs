using MediatR;
using OrderService.Application.DTOs.Response;
using OrderService.Application.Queries;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Handler.QueryHandler
{
	public class GetRevenueByMonthQueryHandlerService : IRequestHandler<GetRevenueByMonthQuery, List<RevenueResponse>>
	{
		private readonly IPaymentRepositoryRead _repository;
		public GetRevenueByMonthQueryHandlerService(IPaymentRepositoryRead repository)
		{
			_repository = repository;
		}
		public async Task<List<RevenueResponse>> Handle(GetRevenueByMonthQuery request, CancellationToken cancellationToken)
		{
			var payments = await _repository.RevenueByMonth(request.StartMonth, request.EndMonth);

			var result = payments
				.GroupBy(p => new { p.PaymentDate.Year, p.PaymentDate.Month })
				.Select(g => new RevenueResponse
				{
					Period = $"{g.Key.Year}-{g.Key.Month:D2}",
					BookingRevenue = g.Where(p => p.BookingId != null).Sum(p => p.Amount),
					OrderRevenue = g.Where(p => p.OrderId != null).Sum(p => p.Amount),
					TotalRevenue = g.Sum(p => p.Amount)
				})
				.ToList();

			return result;
		}
	}
}
