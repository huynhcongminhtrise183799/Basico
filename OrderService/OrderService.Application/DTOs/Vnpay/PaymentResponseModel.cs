using OrderService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.DTOs.Vnpay
{
	public class PaymentResponseModel
	{
		public string OrderDescription { get; set; }
		public string TransactionId { get; set; }
		public string PaymentId { get; set; }
		public bool Success { get; set; }
		public PaymentStatus Status { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public string Token { get; set; }
		public string VnPayResponseCode { get; set; } // 0 : success , 1 : fail
		public Guid TargetId { get; set; }   // Id truyền vào
		public bool IsBooking { get; set; } // true: booking, false: order

		public Guid? AccountId { get; set; }
        public double Amount { get; set; }
	}
}
