using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Consumer.Ticket
{
    public class TicketDeletedConsumerService : IConsumer<TicketDeletedEvent>
    {
        private readonly ITicketRepositoryRead _readRepository;

        public TicketDeletedConsumerService(ITicketRepositoryRead readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task Consume(ConsumeContext<TicketDeletedEvent> context)
        {
            await _readRepository.MarkAsCancelledAsync(context.Message.TicketId);
        }
    }
}
