using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.DTOs.Response;
using TicketService.Application.Queries;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.QueryHandler.TicketQueryHandler
{
    public class GetTicketByTicketIdHandlerService : IRequestHandler<GetTicketByTicketIdQuery, TicketDetail>
    {
        private readonly ITicketRepositoryRead _repoRead;
        public GetTicketByTicketIdHandlerService(ITicketRepositoryRead repoRead)
        {
            _repoRead = repoRead;
        }
        public async Task<TicketDetail> Handle(GetTicketByTicketIdQuery request, CancellationToken cancellationToken)
        {
            var ticket = await _repoRead.GetByIdAsync(request.Id);
            return new TicketDetail
            {
                TicketId = ticket.TicketId,
                UserId = ticket.UserId,
                ServiceId = ticket.ServiceId,
                Content_Send = ticket.Content_Send,
                Status = ticket.Status,

            };
        }
    }
}
