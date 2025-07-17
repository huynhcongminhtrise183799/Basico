using Microsoft.AspNetCore.Http;
using OrderService.Application.DTOs.Vnpay;
using OrderService.Application.IService;
using OrderService.Application.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace OrderService.Application.Service
{
	public class PaymentService : IPaymentService
	{
		private readonly IConfiguration _configuration;
		public PaymentService(IConfiguration configuration)
		{
			_configuration = configuration;	
		}
		public string CreatePaymentUrl(PaymentInformationModel model, HttpContext context)
		{
			var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]);
			var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
			var tick = DateTime.Now.Ticks.ToString();

			var pay = new VnPayLibrary();
			var urlCallBack = _configuration["Vnpay:PaymentBackReturnUrl"];
			//var returnUrl = model.ReturnUrl ?? _configuration["Vnpay:PaymentBackReturnUrl"]; // fallback nếu không có

			var orderType = model.BookingId.HasValue ? "BOOKING" : "ORDER";
			var targetId = model.BookingId ?? model.OrderId;

			pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]);
			pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]);
			pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]);
			pay.AddRequestData("vnp_Amount", ((int)model.Amount * 100).ToString());
			pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
			pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]);
			pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(context));
			pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]);
			pay.AddRequestData("vnp_OrderInfo", $"{orderType}:{targetId}:{model.AccountId}");
			pay.AddRequestData("vnp_OrderType", "other");
			pay.AddRequestData("vnp_ReturnUrl", urlCallBack);
			//pay.AddRequestData("vnp_ReturnUrl", returnUrl);
			pay.AddRequestData("vnp_TxnRef", tick);

			var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"], _configuration["Vnpay:HashSecret"]);
			return paymentUrl;
		}


		public PaymentResponseModel PaymentExecute(IQueryCollection collections)
		{
			var pay = new VnPayLibrary();
			var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);

			return response;
		}
	}
}
