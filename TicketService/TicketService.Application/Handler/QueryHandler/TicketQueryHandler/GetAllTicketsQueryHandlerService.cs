using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Queries;
using TicketService.Domain.Entities;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.QueryHandler.TicketQueryHandler
{
    public class GetAllTicketsQueryHandlerService : IRequestHandler<GetAllTicketsQuery, IEnumerable<Ticket>>
    {
        private readonly ITicketRepositoryRead _repository;

        public GetAllTicketsQueryHandlerService(ITicketRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Ticket>> Handle(GetAllTicketsQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
