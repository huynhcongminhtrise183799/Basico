using OrderService.Application.DTOs.Vnpay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace OrderService.Application.Command
{
	public class CreatePaymentCommand : IRequest<string>
	{
		public PaymentResponseModel Response { get; set; }

		public CreatePaymentCommand(PaymentResponseModel response)
		{
			Response = response;
		}
	}

}
