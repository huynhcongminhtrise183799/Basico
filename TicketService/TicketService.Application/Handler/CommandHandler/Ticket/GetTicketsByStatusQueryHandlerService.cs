using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Application.Queries;
using TicketService.Domain.IRepositories;

namespace TicketService.Application.Handler.CommandHandler.Ticket
{
    public class GetTicketsByStatusQueryHandlerService : IRequestHandler<GetTicketsByStatusQuery, IEnumerable<Domain.Entities.Ticket>>
    {
        private readonly ITicketRepositoryRead _repository;

        public GetTicketsByStatusQueryHandlerService(ITicketRepositoryRead repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Domain.Entities.Ticket>> Handle(GetTicketsByStatusQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetByStatusAsync(request.Status);
        }
    }
}
