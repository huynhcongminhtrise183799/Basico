using Microsoft.AspNetCore.Http;
using OrderService.Application.DTOs.Vnpay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.IService
{
	public interface IPaymentService
	{
		string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
		PaymentResponseModel PaymentExecute(IQueryCollection collections);
	}
}
