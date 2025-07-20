using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Application.Handler.QueryHandler.TicketQueryHandler
{
    public class GetTicketsByCustomerIdQueryService : IRequest<IEnumerable<Ticket>>
    {
        public Guid UserId { get; set; }
    }
}
