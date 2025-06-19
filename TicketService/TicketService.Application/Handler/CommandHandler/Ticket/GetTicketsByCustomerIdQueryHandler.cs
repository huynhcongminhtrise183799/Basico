using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Handler.QueryHandler.TicketQueryHandler;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler.Ticket
{
    public class GetTicketsByCustomerIdQueryHandler : IRequestHandler<GetTicketsByCustomerIdQuery, IEnumerable<Domain.Entities.Ticket>>
    {
        private readonly ITicketRepositoryRead _repository;

        public GetTicketsByCustomerIdQueryHandler(ITicketRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Ticket>> Handle(GetTicketsByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByCustomerIdAsync(request.UserId);
        }
    }
}
