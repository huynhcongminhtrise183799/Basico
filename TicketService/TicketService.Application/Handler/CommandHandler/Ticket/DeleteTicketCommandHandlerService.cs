using Contracts.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Command.Ticket;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler.Ticket
{
    public class DeleteTicketCommandHandlerService : IRequestHandler<DeleteTicketCommand, bool>
    {
        private readonly ITicketRepositoryWrite _repository;
        private readonly IPublishEndpoint _publishEndpoint;

        public DeleteTicketCommandHandlerService(
            ITicketRepositoryWrite repository,
            IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task<bool> Handle(DeleteTicketCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.TicketId);

            await _publishEndpoint.Publish(new TicketDeletedEvent
            {
                TicketId = request.TicketId
            }, cancellationToken);
            return true;
        }
    }
}
