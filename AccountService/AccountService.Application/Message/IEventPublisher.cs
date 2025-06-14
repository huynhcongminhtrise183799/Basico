using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountService.Application.Message
{
    public interface IEventPublisher
    {
        Task Publish<T>(T @event) where T : class;
        IRequestClient<T> CreateRequestClient<T>() where T : class;
    }
}
