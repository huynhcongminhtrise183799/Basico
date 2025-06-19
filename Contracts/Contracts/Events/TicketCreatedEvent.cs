using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class TicketCreatedEvent
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public Guid ServiceId { get; set; }
        public string Content_Send { get; set; }
        public string Status { get; set; }
    }
}
