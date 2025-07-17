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
	public class GetRevenueByYearQueryHandler : IRequestHandler<GetRevenueByYearQuery, List<RevenueResponse>>
	{
		private readonly IPaymentRepositoryRead _paymentRepository;

		public GetRevenueByYearQueryHandler(IPaymentRepositoryRead paymentRepository)
		{
			_paymentRepository = paymentRepository;
		}

		public async Task<List<RevenueResponse>> Handle(GetRevenueByYearQuery request, CancellationToken cancellationToken)
		{
			var payments = await _paymentRepository.RevenueByYear(request.YearFrom, request.YearTo);

			var result = payments
				.GroupBy(p => p.PaymentDate.Year)
				.Select(g => new RevenueResponse
				{
					Period = g.Key.ToString(), 
					BookingRevenue = g.Where(p => p.BookingId != null).Sum(p => p.Amount),
					OrderRevenue = g.Where(p => p.OrderId != null).Sum(p => p.Amount),
					TotalRevenue = g.Sum(p => p.Amount)
				})
				.ToList();
			return result;
		}

	}
}
