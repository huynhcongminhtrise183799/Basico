﻿using OrderService.Domain.Entities;
using OrderService.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Infrastructure.Write.Repositories
{
	public class PaymentRepositoryWrite : IPaymentRepositoryWrite
	{
		private readonly OrderDbContextWrite _context;
		public PaymentRepositoryWrite(OrderDbContextWrite context)
		{
			_context = context;
		}
		public async Task AddPaymentAsync(Payment payment)
		{
			await _context.Payments.AddAsync(payment);
			await _context.SaveChangesAsync();
		}
	}
}
