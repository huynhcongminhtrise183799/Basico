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
    public class ServiceDeletedConsumer : IConsumer<ServiceDeletedEvent>
    {
        private readonly IAccountRepositoryRead _repository;

        public ServiceDeletedConsumer(IAccountRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task Consume(ConsumeContext<ServiceDeletedEvent> context)
        {
            var evt = context.Message;
            await _repository.DeleteServiceAsync(evt.ServiceId);
        }
    }
}
