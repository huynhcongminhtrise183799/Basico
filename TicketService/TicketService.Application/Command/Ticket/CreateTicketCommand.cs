using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Command.Ticket
{
    public class CreateTicketCommand : IRequest<Guid>
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public string Content_Send { get; set; }
    }
}
