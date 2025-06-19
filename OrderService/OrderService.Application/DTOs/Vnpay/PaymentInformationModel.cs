using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs.Vnpay
{
	public class PaymentInformationModel
	{
		public Guid? OrderId { get; set; }
		public Guid? BookingId { get; set; }
		public double Amount { get; set; }
	}
}
	