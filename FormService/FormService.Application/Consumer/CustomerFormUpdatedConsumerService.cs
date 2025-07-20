using FormService.Application.Event;
using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Consumer
{
	public class CustomerFormUpdatedConsumerService : IConsumer<CustomerFormUpdatedEvent>
	{
		private readonly IFormDataRepositoryRead _repoRead;

		public CustomerFormUpdatedConsumerService(IFormDataRepositoryRead repoRead)
		{
			_repoRead = repoRead;
		}

		public async Task Consume(ConsumeContext<CustomerFormUpdatedEvent> context)
		{
			var message = context.Message;
			var customerForm = new CustomerForm
			{
				CustomerFormId = message.CustomerFormId,
				CustomerFormData = message.FormData,
				Status = CustomerFormStatus.USED.ToString() // Assuming USED status for updated forms
			};
			await _repoRead.UpdateAsync(customerForm);
		}
	}
}
