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
    public class UpdateServiceCommandHandler : IRequestHandler<UpdateServiceCommand, bool>
    {
        private readonly IAccountRepositoryWrite _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public UpdateServiceCommandHandler(
            IAccountRepositoryWrite repository,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(UpdateServiceCommand request, CancellationToken cancellationToken)
        {
            var service = await _repository.GetServiceByIdAsync(request.ServiceId);
            if (service != null)
            {
                service.ServiceName = request.ServiceName;
                service.ServiceDescription = request.ServiceDescription;
                var result = await _repository.UpdateServiceAsync(service);
				if (!result)
				{
					return false;
				}

				await _publishEndpoint.Publish(new ServiceUpdatedEvent
                {
                    ServiceId = service.ServiceId,
                    ServiceName = service.ServiceName,
                    ServiceDescription = service.ServiceDescription
                }, cancellationToken);
				return true;
			}
            return false;
        }

       
    }
}
