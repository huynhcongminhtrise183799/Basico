using Contracts.Events;
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
	public class CreateCustomerFormDataConsumer : IConsumer<CreateCustomerFormDataEvent>
	{
		private readonly IFormDataRepositoryWrite _formDataRepositoryWrite;
		private readonly IFormDataRepositoryRead _formDateRepositoryRead;
		private readonly IFormTemplateRepositoryRead _formTemplateRepositoryRead;

		public CreateCustomerFormDataConsumer(
			IFormDataRepositoryWrite formDataRepositoryWrite,
			IFormDataRepositoryRead formDateRepositoryRead,
			IFormTemplateRepositoryRead formTemplateRepositoryRead)
		{
			_formDataRepositoryWrite = formDataRepositoryWrite;
			_formDateRepositoryRead = formDateRepositoryRead;
			_formTemplateRepositoryRead = formTemplateRepositoryRead;
		}
		public async Task Consume(ConsumeContext<CreateCustomerFormDataEvent> context)
		{
			var message = context.Message;
			var template = await _formTemplateRepositoryRead.GetFormTemplateByIdAsync(message.FormTemplateId);
			var customerForm = new CustomerForm
			{
				CustomerFormId = Guid.NewGuid(),
				CustomerId = message.CustomerId,
				FormTemplateId = message.FormTemplateId,
				CustomerFormData = template.FormTemplateData,
				Status = CustomerFormStatus.NOTUSED.ToString(), // Default status
			};
			await _formDataRepositoryWrite.AddAsync(customerForm);
			await _formDateRepositoryRead.AddAsync(customerForm);

		}
	}
}
