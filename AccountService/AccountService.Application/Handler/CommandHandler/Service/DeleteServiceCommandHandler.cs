using AccountService.Application.Commands.Service;
using AccountService.Application.Event.Service;
using AccountService.Domain.IRepositories;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Handler.CommandHandler.Service
{
	public class DeleteServiceCommandHandler : IRequestHandler<DeleteServiceCommand, bool>
	{
		private readonly IAccountRepositoryWrite _repository;
		private readonly IPublishEndpoint _publishEndpoint;

		public DeleteServiceCommandHandler(
			IAccountRepositoryWrite repository,
			IPublishEndpoint publishEndpoint)
		{
			_repository = repository;
			_publishEndpoint = publishEndpoint;
		}

		public async Task<bool> Handle(DeleteServiceCommand request, CancellationToken cancellationToken)
		{
			var service = await _repository.GetServiceByIdAsync(request.ServiceId);
			if (service != null)
			{
				var result = await _repository.DeleteServiceAsync(service.ServiceId);
				if (!result) { return false; }

				await _publishEndpoint.Publish(new ServiceDeletedEvent
				{
					ServiceId = service.ServiceId
				}, cancellationToken);
			}
			return false;
		}


	}
}
