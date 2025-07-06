using AccountService.Domain.IRepositories;
using Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Lawyer
{
	public class GetDetailBookingInformationConsumer : IConsumer<GetDetailBookingInformation>
	{
		private readonly IAccountRepositoryRead _accountRead;
		private readonly IServiceRepositoryRead _serviceRead;

		public GetDetailBookingInformationConsumer(IAccountRepositoryRead accountRead, IServiceRepositoryRead serviceRead)
		{
			_accountRead = accountRead;
			_serviceRead = serviceRead;
		}

		public async Task Consume(ConsumeContext<GetDetailBookingInformation> context)
		{
			var message = context.Message;
			var lawyerName = await _accountRead.GetLaywerNameByLawyerId(message.LawyerId);
			var serviceName = await _serviceRead.GetServiceNameByServiceId(message.ServiceId);
			var customerName = await _accountRead.GetCustomerNameByCustomerId(message.CustomerId);
			await context.RespondAsync(new GetDetailBookingInformation
            {
				CorrelationId = message.CorrelationId,
				LawyerId = message.LawyerId,
				ServiceId = message.ServiceId,
				LawyerName = lawyerName,
				ServiceName = serviceName,
				CustomerName = customerName,
			});
		}
	}
}
