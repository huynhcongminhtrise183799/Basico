﻿using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.IRepositories
{
	public interface IPaymentRepositoryWrite
	{
		Task AddPaymentAsync(Payment payment);
	}
}
