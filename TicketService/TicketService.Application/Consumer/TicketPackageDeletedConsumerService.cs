using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Event;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Consumer
{
    public class TicketPackageDeletedConsumerService : IConsumer<TicketPackageDeletedEvent>
    {
        private readonly ITicketPackageRepositoryRead _repo;
        public TicketPackageDeletedConsumerService(ITicketPackageRepositoryRead repo)
        {
            _repo = repo;
        }
        public async Task Consume(ConsumeContext<TicketPackageDeletedEvent> context)
        {
           await _repo.DeleteAsync(context.Message.id);
        }
    }
}
