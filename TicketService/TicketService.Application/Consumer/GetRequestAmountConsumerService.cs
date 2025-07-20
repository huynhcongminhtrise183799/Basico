using Contracts.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Consumer
{
    public class GetRequestAmountConsumerService : IConsumer<GetRequestAmountEvent>
    {
        private readonly ITicketPackageRepositoryRead _repoRead;
        public GetRequestAmountConsumerService(ITicketPackageRepositoryRead repoRead)
        {
            _repoRead = repoRead;
        }
        public async Task Consume(ConsumeContext<GetRequestAmountEvent> context)
        {
            var ticketPackage = await _repoRead.GetByIdAsync(context.Message.TicketPackageId);
            await context.RespondAsync(new RequestAmountResponseEvent
            {
                RequestAmount = ticketPackage?.RequestAmount ?? 0
            });
        }
    }
}
