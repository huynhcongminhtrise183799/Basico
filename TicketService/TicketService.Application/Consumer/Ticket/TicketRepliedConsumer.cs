using Contracts.Events;
using MassTransit;
using TicketService.Domain.IRepositories;


namespace TicketService.Application.Consumer.Ticket
{
    public class TicketRepliedConsumer : IConsumer<TicketRepliedEvent>
    {
        private readonly ITicketRepositoryRead _readRepository;

        public TicketRepliedConsumer(ITicketRepositoryRead readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task Consume(ConsumeContext<TicketRepliedEvent> context)
        {
            var ticket = await _readRepository.GetByIdAsync(context.Message.TicketId);
            if (ticket == null) return;

            ticket.StaffId = context.Message.StaffId;
            ticket.Content_Response = context.Message.Response;
            ticket.Status = context.Message.Status;

            await _readRepository.UpdateAsync(ticket);
        }
    }
}
