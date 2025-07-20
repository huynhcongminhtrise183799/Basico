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
	public class GetRevenueByDateQueryHandlerService : IRequestHandler<GetRevenueByDateQuery, List<RevenueResponse>>
	{
		private readonly IPaymentRepositoryRead _paymentRepository;
		public GetRevenueByDateQueryHandlerService(IPaymentRepositoryRead paymentRepository)
		{
			_paymentRepository = paymentRepository;
		}
		public async Task<List<RevenueResponse>> Handle(GetRevenueByDateQuery request, CancellationToken cancellationToken)
		{
			var payments = await _paymentRepository.RevenueByDate(request.StartDate, request.EndDate);

			var result = payments
				.GroupBy(p => p.PaymentDate.Date)
				.Select(g => new RevenueResponse
				{
					Period = g.Key.ToString("yyyy-MM-dd"),
					BookingRevenue = g.Where(p => p.BookingId != null).Sum(p => p.Amount),
					OrderRevenue = g.Where(p => p.OrderId != null).Sum(p => p.Amount),
					TotalRevenue = g.Sum(p => p.Amount)
				})
				.ToList();

			return result;
		}
	}
}
