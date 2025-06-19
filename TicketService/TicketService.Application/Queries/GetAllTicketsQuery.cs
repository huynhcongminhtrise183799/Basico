using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketService.Domain.Entities;

namespace TicketService.Application.Queries
{
    public class GetAllTicketsQuery : IRequest<IEnumerable<Ticket>> { }

}
