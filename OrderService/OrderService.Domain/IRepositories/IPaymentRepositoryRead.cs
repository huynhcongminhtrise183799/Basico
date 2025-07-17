using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.IRepositories
{
	public interface IPaymentRepositoryRead
	{
		Task AddPaymentAsync(Payment payment);

		Task<List<Payment>> RevenueByYear(string yearFrom, string yearTo);

		Task<List<Payment>> RevenueByDate(string startDate, string endDate);
		Task<List<Payment>> RevenueByMonth(string startMonth, string endMonth);

	}
}
