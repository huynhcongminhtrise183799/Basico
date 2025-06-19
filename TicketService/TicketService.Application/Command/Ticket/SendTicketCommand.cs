using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Command.Ticket
{
    public class SendTicketCommand : IRequest<bool>
    {
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public string ContentSend { get; set; }
    }
}
