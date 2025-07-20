using Contracts.Events;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Command.Ticket;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler.Ticket
{
    public class CreateTicketCommandHandlerService : IRequestHandler<CreateTicketCommand, Guid>
        {
            private readonly ITicketRepositoryWrite _writeRepository;
            private readonly IPublishEndpoint _publishEndpoint;
            private readonly IClientFactory _clientFactory;

            public CreateTicketCommandHandlerService(ITicketRepositoryWrite writeRepository, IPublishEndpoint publishEndpoint, IClientFactory clientFactory)
            {
                _writeRepository = writeRepository;
                _publishEndpoint = publishEndpoint;
                _clientFactory = clientFactory;
        }

            public async Task<Guid> Handle(CreateTicketCommand command, CancellationToken cancellationToken)
            {

            var validation = new ValidatationRequestTicket
            {
                AccountId = command.UserId,
            };
            var client = _clientFactory.CreateRequestClient<ValidatationRequestTicket>();
            var response = await client.GetResponse<ValidatationRequestTicket>(validation, cancellationToken);
            if (response.Message.IsValid)
            {
                var ticket = new Domain.Entities.Ticket
                {
                    TicketId = Guid.NewGuid(),
                    UserId = command.UserId,
                    ServiceId = command.ServiceId,
                    Content_Send = command.Content_Send,
                    Status = TicketStatus.InProgress.ToString()
                };

                await _writeRepository.AddAsync(ticket);
                await _writeRepository.SaveChangesAsync();

                await _publishEndpoint.Publish(new TicketCreatedEvent
                {
                    TicketId = ticket.TicketId,
                    UserId = ticket.UserId,
                    ServiceId = ticket.ServiceId,
                    Content_Send = ticket.Content_Send,
                    Status = ticket.Status
                }, cancellationToken);

                // Publish the event to notify other services
                await _publishEndpoint.Publish(new DecreseTicketRequestEvent
                {
                    CustomerId = ticket.UserId,
                });
                return ticket.TicketId;
            }


            return Guid.Empty;

        }
    }
}
