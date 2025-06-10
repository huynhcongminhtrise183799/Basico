using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Event;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Consumer
{
    public class TicketPackageCreatedConsumer : IConsumer<TicketPackageCreatedEvent>
    {
        private readonly ITicketPackageRepositoryRead _repo;
        public TicketPackageCreatedConsumer(ITicketPackageRepositoryRead repo)
        {
            _repo = repo ;
        }
        public async Task Consume(ConsumeContext<TicketPackageCreatedEvent> context)
        {
            await _repo.AddAsync(new TicketPackage
            {
                TicketPackageId = context.Message.TicketPackageId,
                TicketPackageName = context.Message.TicketPackageName,
                RequestAmount = context.Message.RequestAmount,
                Price = context.Message.Price,
                Status = context.Message.Status
            });
        }
    }
}
