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
    public class ServiceUpdatedConsumer : IConsumer<ServiceUpdatedEvent>
    {
        private readonly IAccountRepositoryRead _repository;

        public ServiceUpdatedConsumer(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ServiceUpdatedEvent> context)
        {
            var evt = context.Message;
            var service = await _repository.GetServiceByIdAsync(evt.ServiceId);
            if (service != null)
            {
                service.ServiceName = evt.ServiceName;
                service.ServiceDescription = evt.ServiceDescription;
                await _repository.UpdateServiceAsync(service);
            }
        }
    }
}
