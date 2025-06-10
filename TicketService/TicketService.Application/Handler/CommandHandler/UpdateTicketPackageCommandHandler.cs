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
    public class UpdateTicketPackageCommandHandler : IRequestHandler<UpdateTicketPackageCommand, TicketPackageResponse>
    {
        private readonly ITicketPackageRepositoryWrite _repo;
        private readonly IPublishEndpoint _publish;

        public UpdateTicketPackageCommandHandler(ITicketPackageRepositoryWrite repo, IPublishEndpoint publishEndpoint)
        {
            _repo = repo;
            _publish = publishEndpoint;
        }
        public async Task<TicketPackageResponse> Handle(UpdateTicketPackageCommand request, CancellationToken cancellationToken)
        {
            var package = new TicketPackage
            {
                TicketPackageId = request.id,
                TicketPackageName = request.name,
                RequestAmount = request.amount,
                Price = request.price,
                Status = request.status
            };
            await _repo.UpdateAsync(package);
            var event_send = new TicketPackageUpdatedEvent
            {
                TicketPackageId = package.TicketPackageId,
                TicketPackageName = package.TicketPackageName,
                RequestAmount = package.RequestAmount,
                Price = package.Price,
                Status = package.Status
            };
            await _publish.Publish(event_send);
            var response = new TicketPackageResponse
            {
                TicketPackageId = package.TicketPackageId,
                TicketPackageName = package.TicketPackageName,
                RequestAmount = package.RequestAmount,
                Price = package.Price,
                Status = package.Status
            };
            return response;
        }
    }
}
