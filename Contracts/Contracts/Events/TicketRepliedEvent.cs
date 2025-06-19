using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.Events
{
    public class TicketRepliedEvent
    {
        public Guid TicketId { get; set; }
        public Guid StaffId { get; set; }
        public string Response { get; set; } = string.Empty;
        public string Status { get; set; } = "ANSWERED"; 
    }
}
