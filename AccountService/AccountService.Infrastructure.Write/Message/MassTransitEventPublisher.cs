using AccountService.Application.Message;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Infrastructure.Write.Message
{
    public class MassTransitEventPublisher : IEventPublisher
    {
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IClientFactory _clientFactory;
        public MassTransitEventPublisher(IPublishEndpoint publishEndpoint, IClientFactory clientFactory)
        {
            _publishEndpoint = publishEndpoint;
            _clientFactory = clientFactory;
        }

        public Task Publish<T>(T @event) where T : class
        {
            return _publishEndpoint.Publish(@event);
        }

        public IRequestClient<T> CreateRequestClient<T>() where T : class
        {
            return _clientFactory.CreateRequestClient<T>();
        }
    }
}
