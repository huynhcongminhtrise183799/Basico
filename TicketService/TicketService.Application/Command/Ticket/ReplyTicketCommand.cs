using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Command.Ticket
{
    public class ReplyTicketCommand : IRequest<bool>
    {
        public Guid TicketId { get; set; }
        public Guid StaffId { get; set; }
        public string Response { get; set; }
    }
}
