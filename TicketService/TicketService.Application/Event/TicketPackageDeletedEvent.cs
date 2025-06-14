using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketService.Application.Event
{
    public class TicketPackageDeletedEvent
    {
        public Guid id { get; set; }

    }
}
