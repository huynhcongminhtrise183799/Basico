using AccountService.Application.Event.Service;
using AccountService.Domain.IRepositories;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Consumers.Service
{
    public class ServiceCreatedConsumerService : IConsumer<ServiceCreatedEvent>
    {
        private readonly IAccountRepositoryRead _repository;

        public ServiceCreatedConsumerService(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ServiceCreatedEvent> context)
        {
            var evt = context.Message;

            var service = new Domain.Entity.Service
            {
                ServiceId = evt.ServiceId,
                ServiceName = evt.ServiceName,
                ServiceDescription = evt.ServiceDescription,
				Status = evt.Status,
			};

            await _repository.AddServiceAsync(service);
        }
    }


}
