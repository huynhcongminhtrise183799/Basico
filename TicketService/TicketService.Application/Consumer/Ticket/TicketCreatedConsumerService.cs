using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Consumer.Ticket
{
    public class TicketCreatedConsumerService : IConsumer<TicketCreatedEvent>
    {
        private readonly ITicketRepositoryRead _readDbContext;

        public TicketCreatedConsumerService(ITicketRepositoryRead readDbContext)
        {
            _readDbContext = readDbContext;
        }

        public async Task Consume(ConsumeContext<TicketCreatedEvent> context)
        {
            var ticket = new Domain.Entities.Ticket
            {
                TicketId = context.Message.TicketId,
                UserId = context.Message.UserId,
                ServiceId = context.Message.ServiceId,
                Content_Send = context.Message.Content_Send,
                Status = context.Message.Status
            };

            await _readDbContext.AddAsync(ticket);
        }
    }
}
