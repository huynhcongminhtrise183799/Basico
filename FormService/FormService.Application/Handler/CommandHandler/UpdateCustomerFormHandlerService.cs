using FormService.Application.Command;
using FormService.Application.Event;
using FormService.Domain.Entities;
using FormService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormService.Application.Handler.CommandHandler
{
	public class UpdateCustomerFormHandlerService : IRequestHandler<UpdateCustomerFormCommand, bool>
	{
		private readonly IFormDataRepositoryWrite _repoWrite;
		private readonly IPublishEndpoint _publishEndpoint;
		public UpdateCustomerFormHandlerService(IFormDataRepositoryWrite repoWrite, IPublishEndpoint publishEndpoint)
		{
			_repoWrite = repoWrite;
			_publishEndpoint = publishEndpoint;
		}
		public async Task<bool> Handle(UpdateCustomerFormCommand request, CancellationToken cancellationToken)
		{
			var customerForm = new CustomerForm
			{
				CustomerFormId = request.CustomerFormId,
				CustomerFormData = request.FormData,
				Status = CustomerFormStatus.USED.ToString()
			};

			var result = await _repoWrite.UpdateAsync(customerForm);
			if (!result)
			{
				return false;
			}
			var @event = new CustomerFormUpdatedEvent
			{
				CustomerFormId = request.CustomerFormId,
				FormData = request.FormData
			};
			await _publishEndpoint.Publish(@event, cancellationToken);
			return true;
		}
	}

}
