using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Read.Repositories
{
	public class PaymentRepositoryRead : IPaymentRepositoryRead
	{
		private readonly OrderDbContextRead _context;
		public PaymentRepositoryRead(OrderDbContextRead context)
		{
			_context = context;
		}
		public async Task AddPaymentAsync(Payment payment)
		{
			await _context.Payments.AddAsync(payment);
			await _context.SaveChangesAsync();
		}

		public async Task<List<Payment>> RevenueByDate(string startDate, string endDate)
		{
			var startDateParsed = DateTime.SpecifyKind(DateTime.Parse(startDate), DateTimeKind.Utc);
			var endDateParsed = DateTime.SpecifyKind(DateTime.Parse(endDate).AddDays(1).AddTicks(-1), DateTimeKind.Utc);
			return await _context.Payments
				.Where(p => p.PaymentDate >= startDateParsed && p.PaymentDate <= endDateParsed)
				.ToListAsync();
		}

		public async Task<List<Payment>> RevenueByMonth(string startMonth, string endMonth)
		{
			var startMonthDate = DateTime.SpecifyKind(
		DateTime.ParseExact(startMonth, "yyyy-MM", null),
		DateTimeKind.Utc
	);

			var endMonthDate = DateTime.SpecifyKind(
				DateTime.ParseExact(endMonth, "yyyy-MM", null)
					.AddMonths(1)
					.AddTicks(-1),
				DateTimeKind.Utc
			);

			return await _context.Payments
				.Where(p => p.PaymentDate >= startMonthDate && p.PaymentDate <= endMonthDate)
				.ToListAsync();
		}


		public async Task<List<Payment>> RevenueByYear(string yearFrom, string yearTo)
		{
			var yearFromDate = DateTime.SpecifyKind(
		new DateTime(int.Parse(yearFrom), 1, 1),
		DateTimeKind.Utc
	);

			var yearToDate = DateTime.SpecifyKind(
				new DateTime(int.Parse(yearTo), 1, 1)
					.AddYears(1)
					.AddTicks(-1),
				DateTimeKind.Utc
			);
			return await _context.Payments
				.Where(p => p.PaymentDate >= yearFromDate && p.PaymentDate <= yearToDate)
				.ToListAsync();
		}

	}
}
