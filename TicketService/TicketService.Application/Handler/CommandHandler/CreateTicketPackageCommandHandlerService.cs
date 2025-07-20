using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Command;
using TicketService.Application.DTOs.Response;
using TicketService.Application.Event;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler
{
    public class CreateTicketPackageCommandHandlerService : IRequestHandler<CreateTicketPackageCommand, TicketPackageResponse>
    {
        private readonly ITicketPackageRepositoryWrite _repo;
        private readonly IPublishEndpoint _publish;
        public CreateTicketPackageCommandHandlerService(ITicketPackageRepositoryWrite repo, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _publish = publishEndpoint;
        }
        public async Task<TicketPackageResponse> Handle(CreateTicketPackageCommand request, CancellationToken cancellationToken)
        {
            var ticketPackage = new TicketPackage
            {
                TicketPackageId = Guid.NewGuid(),
                TicketPackageName = request.name,
                RequestAmount = request.amount,
                Price = request.price,
                Status = Status.ACTIVE.ToString()
            };

            var result = await _repo.AddAsync(ticketPackage);
			if (!result)
			{
				return null; 
			}

			var response = new TicketPackageResponse
            {
                TicketPackageId = ticketPackage.TicketPackageId,
                TicketPackageName = ticketPackage.TicketPackageName,
                RequestAmount = ticketPackage.RequestAmount,
                Price = ticketPackage.Price,
                Status = ticketPackage.Status
            };
            var event_send = new TicketPackageCreatedEvent
            {
                TicketPackageId = ticketPackage.TicketPackageId,
                TicketPackageName = ticketPackage.TicketPackageName,
                RequestAmount = ticketPackage.RequestAmount,
                Price = ticketPackage.Price,
                Status = ticketPackage.Status
            };
            await _publish.Publish(event_send);

            return response;
        }
    }
}
