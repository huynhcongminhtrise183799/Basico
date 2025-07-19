using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Command;
using TicketService.Application.Event;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler
{
    public class DeleteTicketPackageCommandHandler : IRequestHandler<DeleteTicketPackageCommand, bool>
    {
        private readonly ITicketPackageRepositoryWrite _repo;
        private readonly IPublishEndpoint _publish;

        public DeleteTicketPackageCommandHandler(ITicketPackageRepositoryWrite repo, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _publish = publishEndpoint;
        }
        public async Task<bool> Handle(DeleteTicketPackageCommand request, CancellationToken cancellationToken)
        {
           var result = await _repo.DeleteAsync(request.id);
			if (!result)
			{
				return false; 
			}
			var event_send = new TicketPackageDeletedEvent
            {
                id = request.id
            };
            await _publish.Publish(event_send);
            return true;
        }
    }
}
