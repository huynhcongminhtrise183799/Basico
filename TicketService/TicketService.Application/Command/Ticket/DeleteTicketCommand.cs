using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Command.Ticket
{
    public class DeleteTicketCommand : IRequest<bool>
    {
        public Guid TicketId { get; set; }
    }
}
